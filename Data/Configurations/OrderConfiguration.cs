using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Models;
namespace OrderManagementSystem.Data.Configurations
 
{
    public class OrderConfiguration:IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.OrderID);

            builder.Property(x => x.OrderNo)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(x => x.OrderNo)
                .IsUnique();

            builder.Property(x => x.Status)
            .IsRequired();

            builder.HasMany(x => x.OrderDetails)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
