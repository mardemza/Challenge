using Challenge.Core.Domain;
using Challenge.EntityFramework.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Application.Permissions.Commands
{
    public class ModifyPermissionCommandHandler : IRequestHandler<ModifyPermissionCommand, long>
    {
        private readonly IRepository<Permission> _permissionRepository;
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<PermissionType> _permissionTypeRepository;

        public ModifyPermissionCommandHandler(IRepository<Permission> permissionRepository, IRepository<Employee> employeeRepository, IRepository<PermissionType> permissionTypeRepository)
        {
            _permissionRepository = permissionRepository;
            _employeeRepository = employeeRepository;
            _permissionTypeRepository = permissionTypeRepository;
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

            return permission.Id;

        }
    }
}
