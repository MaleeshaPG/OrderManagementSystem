using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Data.Configurations
{
    public class ItemConfiguration: IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(x => x.ItemID);

            builder.Property(x => x.ItemName)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.SellingPrice)
                .HasPrecision(18, 2);

            builder.Property(x => x.BaseUnitToUnitConversion)
                .HasPrecision(18, 4);

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.IsDeleted)
                .IsRequired();

            builder.HasOne(x => x.SubDepartment)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.SubDepartmentID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.OrderGroup)
             .WithMany(x => x.Items)
             .HasForeignKey(x => x.OrderGroupID)
             .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new
            {
                x.SubDepartmentID,
                x.OrderGroupID,
                x.ItemName
            })
            .IsUnique();
        }
    }
}
