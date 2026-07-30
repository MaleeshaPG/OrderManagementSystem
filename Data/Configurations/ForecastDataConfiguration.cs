using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Data.Configurations
{
    public class ForecastDataConfiguration: IEntityTypeConfiguration<ForecastData>
    {
        public void Configure(
           EntityTypeBuilder<ForecastData> builder)
        {
            builder.HasKey(x => x.ForecastDataID);

            builder.Property(x => x.ForecastQuantity)
                .HasPrecision(18, 2);

            builder.Property(x => x.BufferQuantity)
                .HasPrecision(18, 2);

            builder.Property(x => x.PromotionQuantity)
                .HasPrecision(18, 2);

            builder.HasOne(x => x.StoreItem)
                .WithMany(x => x.ForecastData)
                .HasForeignKey(x => x.StoreItemID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new
            {
                x.StoreItemID,
                x.ForecastDate
            })
            .IsUnique();
        }
    }
}
