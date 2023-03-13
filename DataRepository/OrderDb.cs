using Microsoft.EntityFrameworkCore;
using OrderMicroservice.Models;

namespace DataRepository;
public class OrderDb : DbContext
{
    public OrderDb(DbContextOptions<OrderDb> options)
        : base(options) { }

    public DbSet<Order> Orders => Set<Order>();
}
