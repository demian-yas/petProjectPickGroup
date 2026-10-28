using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PickGroup.Domain.Orders;
using PickGroup.Domain.Orders.OrdersIds;

namespace PickGroup.Infrastructure.Configurations.OrderAgConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");
        
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .HasConversion(
                id => id.Value,
                value => OrderId.Create(value));
        
        
    }
}