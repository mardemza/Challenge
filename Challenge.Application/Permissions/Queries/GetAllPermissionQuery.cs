using Challenge.Application.Permissions.Queries.Dto;
using MediatR;

namespace Challenge.Application.Permissions.Queries
{
    public class GetAllPermissionQuery: IRequest<IEnumerable<EmployeePermissionTypeDto>>
    {
        public string Search { get; set; } = string.Empty;
        public long? EmployeeId { get; set; }
        public long? PermissionTypeId { get; set; }
    }
}
