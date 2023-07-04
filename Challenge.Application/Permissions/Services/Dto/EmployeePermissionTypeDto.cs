using Challenge.Core;

namespace Challenge.Application.Permissions.Services.Dto
{
    public class EmployeePermissionTypeDto: EntityBase
    {
        public string Name { get; set; } = string.Empty;

        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string EmployeeSurname { get; set; } = string.Empty;

        public long PermissionTypeId { get; set; }
        public string PermissionTypeName { get; set; } = string.Empty;
    }
}
