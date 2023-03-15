using DataContext;
using DataContext.Models;
using Microsoft.EntityFrameworkCore;
using OrderMicroservice.DTOs;
using OrderMicroservice.Interfaces;

namespace OrderMicroservice.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDb _db;

        public OrderRepository(OrderDb db) => _db = db;

        public async Task<Order> Create(Order order)
        {
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
            return order;
        }
        public List<Order> GetAll()
        {
            return _db.Orders.ToList();
        }
    }
}
