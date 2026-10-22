using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PickGroup.Domain.Orders;

namespace PickGroup.Infrastructure;

public class ApplicationDbContext(IConfiguration configuration) : DbContext
{
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
}