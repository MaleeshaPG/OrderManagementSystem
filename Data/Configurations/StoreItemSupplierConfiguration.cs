using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Models;
namespace OrderManagementSystem.Data.Configurations
{
    public class StoreItemSupplierConfiguration : IEntityTypeConfiguration<StoreItemSupplier>
    {
        public void Configure(
          EntityTypeBuilder<StoreItemSupplier> builder)
        {
            builder.HasKey(x => x.StoreItemSupplierID);

            builder.HasOne(x => x.StoreItem)
                .WithMany(x => x.StoreItemSuppliers)
                .HasForeignKey(x => x.StoreItemID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Supplier)
                .WithMany(x => x.StoreItemSuppliers)
                .HasForeignKey(x => x.SupplierID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new
            {
                x.StoreItemID,
                x.SupplierID
            })
            .IsUnique();

            builder.Property(x => x.BuyingPrice)
                .HasPrecision(18, 2);
        }
    }
}
