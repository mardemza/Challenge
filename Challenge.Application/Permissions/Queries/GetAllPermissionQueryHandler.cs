using AutoMapper;
using Challenge.Application.Permissions.Services.Dto;
using Challenge.Core.Domain;
using Challenge.EntityFramework.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Application.Permissions.Queries
{
    public class GetAllPermissionQueryHandler : IRequestHandler<GetAllPermissionQuery, IEnumerable<EmployeePermissionTypeDto>>
    {
        private readonly IReadRepository<Permission> _permissionReadRepository;
        private readonly IMapper _mapper;

        public GetAllPermissionQueryHandler(IReadRepository<Permission> permissionReadRepository, IMapper mapper)
        {
            _permissionReadRepository = permissionReadRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeePermissionTypeDto>> Handle(GetAllPermissionQuery request, CancellationToken cancellationToken)
        {
            // -- Check if exist search
            var existSearch = !string.IsNullOrEmpty(request.Search);

            // -- Return elements
            var list = await _permissionReadRepository.GetAllIncluding(x => x.Employee,x => x.PermissionType)   
                                                      .Where(x => !request.EmployeeId.HasValue || 
                                                                  (request.EmployeeId.HasValue && x.EmployeeId == request.EmployeeId))
                                                      .Where(x => !request.PermissionTypeId.HasValue ||
                                                                  (request.PermissionTypeId.HasValue && x.PermissionTypeId == request.PermissionTypeId))
                                                      .Where(x => !existSearch || 
                                                                  (existSearch && (x.Name + " " + 
                                                                                   x.Employee.Name + " " + 
                                                                                   x.Employee.Surname + " " + 
                                                                                   x.PermissionType.Name)
                                                                                   .ToLower()
                                                                                   .Contains(request.Search
                                                                                                    .ToLower()
                                                                                                    .Trim())))            
                                                      .ToListAsync(cancellationToken: cancellationToken);

            if (list == null)
            {
                return new List<EmployeePermissionTypeDto>();
            }

            // -- Mapper to object to result
            var result = list.Select(x => _mapper.Map<EmployeePermissionTypeDto>(x)).AsEnumerable();
            return result;
        }
    }
}
