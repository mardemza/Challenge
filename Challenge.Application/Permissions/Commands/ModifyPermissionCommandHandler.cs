using AutoMapper;
using Challenge.Application.Permissions.Services;
using Challenge.Application.Permissions.Services.Dto;
using Challenge.Core.Domain;
using Challenge.EntityFramework.Interfaces;
using MediatR;

namespace Challenge.Application.Permissions.Commands
{
    public class ModifyPermissionCommandHandler : IRequestHandler<ModifyPermissionCommand, long>
    {
        private readonly IRepository<Permission> _permissionRepository;
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<PermissionType> _permissionTypeRepository;
        private readonly IEslasticsearchService _eslasticsearchService;
        private readonly IMapper _mapper;

        public ModifyPermissionCommandHandler(IRepository<Permission> permissionRepository, IRepository<Employee> employeeRepository, IRepository<PermissionType> permissionTypeRepository, IEslasticsearchService eslasticsearchService, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _employeeRepository = employeeRepository;
            _permissionTypeRepository = permissionTypeRepository;
            _eslasticsearchService = eslasticsearchService;
            _mapper = mapper;
        }


        public async Task<long> Handle(ModifyPermissionCommand request, CancellationToken cancellationToken)
        {
            // -- Check if exist employee
            var existEmployee = await _employeeRepository.AnyAsync(x => x.Id == request.EmployeeId, cancellationToken);
            if (!existEmployee)
                throw new Exception("No existe el empleado seleccionado");

            // -- Check if exist permission type
            var existPermissionType = await _permissionTypeRepository.AnyAsync(x => x.Id == request.PermissionTypeId, cancellationToken);
            if (!existPermissionType)
                throw new Exception("No existe el tipo de permiso seleccionado");

            // -- Check if exist permission
            var existPermission = await _permissionRepository.AnyAsync(x => x.Id != request.Id && x.EmployeeId == request.EmployeeId && x.PermissionTypeId == request.PermissionTypeId, cancellationToken);
            if (existPermission)
                throw new Exception("Ya existe este permiso creado para el empleado seleccionado");

            var permission = await _permissionRepository.GetByIdAsync(request.Id, cancellationToken) ?? throw new Exception("No se puede actualizar este permiso, el registro id no existe");
            permission.EmployeeId = request.EmployeeId;
            permission.PermissionTypeId = request.PermissionTypeId;
            permission.Name = request.Name;

            var result = await _permissionRepository.UpdateAsync(permission, cancellationToken);

            // -- Call Unit Of Work to finish operation
            await _permissionRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            // -- Add on Eslasticsearch 
            var obj = _mapper.Map<PermissionDto>(permission);
            await _eslasticsearchService.Update(obj);

            return permission.Id;

        }
    }
}
