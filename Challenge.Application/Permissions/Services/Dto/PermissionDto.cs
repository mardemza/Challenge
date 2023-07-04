using Challenge.Core;
using Challenge.Core.Domain;

namespace Challenge.Application.Permissions.Services.Dto
{
    public class PermissionDto: EntityBase
    {
        public string Name { get; set; } = string.Empty;

        public long EmployeeId { get; set; }

        public long PermissionTypeId { get; set; }
    }
}
