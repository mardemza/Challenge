namespace Challenge.Core.Domain;

public class Employee: EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;

    public virtual IList<Permission> Permissions { get; set; } = new List<Permission>();
}
