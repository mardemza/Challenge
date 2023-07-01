using Challenge.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.EntityFramework.Config
{
    public class PermissionConfig : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Employee)
                   .WithMany(x => x.Permissions)
                   .HasForeignKey(x => x.EmployeeId);

            builder.HasOne(x => x.PermissionType)
                   .WithMany(x => x.Permissions)
                   .HasForeignKey(x => x.PermissionTypeId);
        }
    }
}