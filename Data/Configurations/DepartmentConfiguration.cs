using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Data.Configurations
{
    public class DepartmentConfiguration: IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
          
            builder.HasKey(x => x.DepartmentID);

            builder.Property(x => x.DepartmentName)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(x => x.DepartmentName)
                .IsUnique();

            builder.Property(x => x.Status)
                .IsRequired();

            builder.HasMany(x => x.SubDepartments)
                .WithOne(x => x.Department)
                .HasForeignKey(x => x.DepartmentID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
