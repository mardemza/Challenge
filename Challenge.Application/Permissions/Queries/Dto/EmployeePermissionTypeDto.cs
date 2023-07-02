namespace Challenge.Application.Permissions.Queries.Dto
{
    public class EmployeePermissionTypeDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string EmployeeSurname { get; set; } = string.Empty;
        
        public long PermissionTypeId { get; set; }
        public string PermissionTypeName { get; set; } = string.Empty;        
    }
}
