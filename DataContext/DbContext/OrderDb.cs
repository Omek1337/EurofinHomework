﻿using DataContext.Models;
using Microsoft.EntityFrameworkCore;

namespace DataContext;
public class OrderDb : DbContext
{
    public OrderDb(DbContextOptions<OrderDb> options)
        : base(options) { }

    public DbSet<Order> Orders => Set<Order>();
}
