using Challenge.Application.Permissions.Services.Dto;
using MediatR;

namespace Challenge.Application.Permissions.Queries
{
    public class GetAllPermissionElasticQuery: IRequest<IEnumerable<EmployeePermissionTypeDto>>
    {
        public string Search { get; set; } = string.Empty;
        public long? EmployeeId { get; set; }
        public long? PermissionTypeId { get; set; }
    }
}
