using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Data.Configurations
{
    public class StoreOrderGroupConfiguration: IEntityTypeConfiguration<StoreOrderGroup>
    {
        public void Configure(
           EntityTypeBuilder<StoreOrderGroup> builder)
        {

            builder.HasKey(x => new
            {
                x.StoreID,
                x.OrderGroupID
            });

            builder.HasOne(x => x.Store)
                .WithMany(x => x.StoreOrderGroups)
                .HasForeignKey(x => x.StoreID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.OrderGroup)
                .WithMany(x => x.StoreOrderGroups)
                .HasForeignKey(x => x.OrderGroupID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
