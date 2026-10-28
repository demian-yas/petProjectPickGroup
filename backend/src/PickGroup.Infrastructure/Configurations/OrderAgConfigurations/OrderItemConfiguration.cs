using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PickGroup.Domain.Orders;
using PickGroup.Domain.Orders.OrdersIds;

namespace PickGroup.Infrastructure.Configurations.OrderAgConfigurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("order_items", table =>
        {
            table.HasCheckConstraint(
                "ck_order_items_required_quantity_positive",
                "require_quantity > 0");
        });
        
        builder.HasKey(o => o.Id);
        
        builder.Property(o => o.Id)
            .HasConversion(
                i => i.Value,
                value => OrderItemId.Create(value));

        builder.Property(o => o.ExternalId);

        builder.Property(o => o.StorageLocationCode)
            .IsRequired();
        
        builder.Property(o => o.ProductName)
            .IsRequired();

        builder.Property(o => o.RequiresManufactureDate)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(o => o.Comment);
        
        builder.Property(o => o.RequiredQuantity)
            .IsRequired();
        
        builder.Property(o => o.Unit)
            .HasConversion<string>()
            .IsRequired();
    }
}