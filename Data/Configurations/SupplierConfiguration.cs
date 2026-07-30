using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Data.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {

            builder.HasKey(x => x.SupplierID);

            builder.Property(x => x.SupplierName)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.Address)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(x => x.TelNo)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(150)
                .IsRequired();

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.IsDeleted)
                .IsRequired();
        }
    }
}
