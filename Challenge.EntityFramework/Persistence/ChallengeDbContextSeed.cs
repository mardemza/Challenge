using Challenge.Core.Domain;

namespace Challenge.EntityFramework.Persistence
{
    public static class ChallengeDbContextSeed
    {
        public static void Seed(this ChallengeDbContext context)
        {
            if (context != null)
            {

                // -- Add Employees
                var employee1 = context.Employees.FirstOrDefault(x => x.Name == "Daniel" && x.Surname == "Lafinur");
                if (employee1 == null)
                {
                    employee1 = new Employee()
                    {
                        Name = "Daniel",
                        Surname = "Lafinur"
                    };
                    context.Employees.Add(employee1);
                    context.SaveChanges();
                }

                var employee2 = context.Employees.FirstOrDefault(x => x.Name == "Lucas" && x.Surname == "Roca");
                if (employee2 == null)
                {
                    employee2 = new Employee()
                    {
                        Name = "Lucas",
                        Surname = "Roca"
                    };
                    context.Employees.Add(employee2);
                    context.SaveChanges();
                }

                var employee3 = context.Employees.FirstOrDefault(x => x.Name == "Lucia" && x.Surname == "Rivadavia");
                if (employee3 == null)
                {
                    employee3 = new Employee()
                    {
                        Name = "Lucia",
                        Surname = "Rivadavia"
                    };
                    context.Employees.Add(employee3);
                    context.SaveChanges();
                }

                // -- Add Permission Type
                var permissionType1 = context.PermissionTypes.FirstOrDefault(x => x.Name == "Administrador");
                if (permissionType1 == null)
                {
                    permissionType1 = new PermissionType()
                    {
                        Name = "Administrador"
                    };
                    context.PermissionTypes.Add(permissionType1);
                    context.SaveChanges();
                }

                var permissionType2 = context.PermissionTypes.FirstOrDefault(x => x.Name == "Supervisor");
                if (permissionType2 == null)
                {
                    permissionType2 = new PermissionType()
                    {
                        Name = "Supervisor"
                    };
                    context.PermissionTypes.Add(permissionType2);
                    context.SaveChanges();
                }
                var permissionType3 = context.PermissionTypes.FirstOrDefault(x => x.Name == "Cajero");
                if (permissionType3 == null)
                {
                    permissionType3 = new PermissionType()
                    {
                        Name = "Cajero"
                    };
                    context.PermissionTypes.Add(permissionType3);
                    context.SaveChanges();
                }

                // -- Add Relation with Permission
                var permission1 = context.Permissions.FirstOrDefault(x => x.EmployeeId == employee1.Id && x.PermissionTypeId == permissionType1.Id);
                if (permission1 == null)
                {

                    permission1 = new Permission()
                    {
                        Name = "Admin",
                        EmployeeId = employee1.Id,
                        PermissionTypeId = permissionType1.Id
                    };
                    context.Permissions.Add(permission1);
                    context.SaveChanges();
                }
                var permission2 = context.Permissions.FirstOrDefault(x => x.EmployeeId == employee1.Id && x.PermissionTypeId == permissionType2.Id);
                if (permission2 == null)
                {

                    permission2 = new Permission()
                    {
                        Name = "Supervisar",
                        EmployeeId = employee1.Id,
                        PermissionTypeId = permissionType2.Id
                    };
                    context.Permissions.Add(permission2);
                    context.SaveChanges();
                }
                var permission3 = context.Permissions.FirstOrDefault(x => x.EmployeeId == employee2.Id && x.PermissionTypeId == permissionType2.Id);
                if (permission3 == null)
                {

                    permission3 = new Permission()
                    {
                        Name = "Supervisar",
                        EmployeeId = employee2.Id,
                        PermissionTypeId = permissionType2.Id
                    };
                    context.Permissions.Add(permission3);
                    context.SaveChanges();
                }
                
            }
        }
    }
}
