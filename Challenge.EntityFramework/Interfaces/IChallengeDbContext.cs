using Challenge.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.EntityFramework.Interfaces
{
    public interface IChallengeDbContext: IUnitOfWork
    {
        DbSet<Employee> Employees { get; }
        DbSet<Permission> Permissions { get; }
        DbSet<PermissionType> PermissionTypes { get; }
    }
}
