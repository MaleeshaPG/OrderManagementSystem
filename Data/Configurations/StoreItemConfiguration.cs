using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Data.Configurations
{
    public class StoreItemConfiguration: IEntityTypeConfiguration<StoreItem>
    {
        public void Configure(
          EntityTypeBuilder<StoreItem> builder)
        {
            builder.HasKey(x => x.StoreItemID);

            builder.HasOne(x => x.Store)
                .WithMany(x => x.StoreItems)
                .HasForeignKey(x => x.StoreID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Item)
                .WithMany(x => x.StoreItems)
                .HasForeignKey(x => x.ItemID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new
            {
                x.StoreID,
                x.ItemID
            })
            .IsUnique();
        }
    }
}
