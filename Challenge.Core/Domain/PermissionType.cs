namespace Challenge.Core.Domain;

public class PermissionType: EntityBase
{
    public string Name { get; set; } = string.Empty;
    public virtual IList<Permission> Permissions { get; set; } = new List<Permission>();
}
