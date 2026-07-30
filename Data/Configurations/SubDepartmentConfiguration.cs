using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Data.Configurations
{
    public class SubDepartmentConfiguration : IEntityTypeConfiguration<SubDepartment>
    {
        public void Configure(EntityTypeBuilder<SubDepartment> builder)
        { 
            builder.HasKey(x => x.SubDepartmentID);

            builder.Property(x => x.SubDepartmentName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();

            builder.HasMany(x => x.Items)
                   .WithOne(x => x.SubDepartment)
                   .HasForeignKey(x => x.SubDepartmentID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new
            {
                x.DepartmentID,
                x.SubDepartmentName
            }).IsUnique();
        }
    }
}
