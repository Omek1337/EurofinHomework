using DataContext;
using DataContext.Models;
using Microsoft.EntityFrameworkCore;
using OrderMicroservice.DTOs;

namespace OrderMicroservice.Interfaces
{
    public interface IOrderRepository
    {
        public Task<Order> Create(Order order);
        public List<Order> GetAll();
    }
}
