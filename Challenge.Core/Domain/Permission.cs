namespace Challenge.Core.Domain;

public class Permission: EntityBase
{
    public string Name { get; set; } = string.Empty;

    public long EmployeeId { get; set; }
    public virtual Employee Employee { get; set;}

    public long PermissionTypeId { get; set; }
    public virtual PermissionType PermissionType { get; set; }
}
