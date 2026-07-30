using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Data.Configurations
{
    public class OrderGroupConfiguration: IEntityTypeConfiguration<OrderGroup>
    {
        public void Configure(EntityTypeBuilder<OrderGroup> builder)
        {
            builder.HasKey(x => x.OrderGroupID);


            builder.Property(x => x.OrderGroupName)
                .HasMaxLength(100)
                .IsRequired();


            builder.HasIndex(x => x.OrderGroupName)
                .IsUnique();


            builder.Property(x => x.Status)
                .IsRequired();


            builder.HasMany(x => x.Items)
                .WithOne(x => x.OrderGroup)
                .HasForeignKey(x => x.OrderGroupID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
