using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PickGroup.Domain.Orders;
using PickGroup.Domain.Orders.OrdersIds;

namespace PickGroup.Infrastructure.Configurations.OrderAgConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders", table =>
        {
            table.HasCheckConstraint(
                "ck_orders_version_positive",
                "version > 0");
        });
        
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .HasConversion(
                id => id.Value,
                value => OrderId.Create(value));
        
        builder.Property(o => o.OrderNumber)
            .IsRequired();

        builder.Property(o => o.ExternalId);
        
        builder.Property(o => o.ManagerName)
            .IsRequired();
        
        builder.Property(o => o.ManagerPhoneNumber)
            .IsRequired();

        builder.HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey("OrderId");
        
        builder.Navigation(o => o.Items)
            .HasField("_items")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
        
        builder.Property(o => o.Status)
            .HasConversion<string>()
            .IsRequired();
        
        builder.Property(o => o.Version)
            .IsRequired();
        
        builder.Property(o => o.DeliveryAddress)
            .IsRequired();

        builder.OwnsOne(o => o.DeliverySchedule, a =>
        {
            a.Property(d => d.StartTime).HasColumnName("delivery_start_time");
            a.Property(d => d.EndTime).HasColumnName("delivery_end_time");
            a.Property(d => d.Shift).HasColumnName("delivery_shift");
        });

        builder.Property(o => o.CreatedAt)
            .HasDefaultValueSql("NOW()");

        builder.Property(o => o.UpdatedAt)
            .HasDefaultValueSql("NOW()");
    }
}