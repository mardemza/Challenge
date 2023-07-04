using Challenge.Core;

namespace Challenge.Application.Permissions.Services.Dto
{
    public class EmployeeDto: EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
    }
}
