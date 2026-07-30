using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Data.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>

    {
        public void Configure(
           EntityTypeBuilder<OrderDetail> builder)
        {

            builder.HasKey(x => x.OrderDetailID);

            builder.Property(x => x.BuyingPrice)
                .HasPrecision(18, 2);

            builder.Property(x => x.BaseUnitToUnitConversion)
                .HasPrecision(18, 4);

            builder.Property(x => x.Quantity)
                .HasPrecision(18, 2);

            builder.HasOne(x => x.Order)
                .WithMany(x => x.OrderDetails)
                .HasForeignKey(x => x.OrderID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Item)
                .WithMany()
                .HasForeignKey(x => x.ItemID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Store)
                .WithMany()
                .HasForeignKey(x => x.StoreID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Supplier)
                .WithMany()
                .HasForeignKey(x => x.SupplierID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
