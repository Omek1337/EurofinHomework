using OrderMicroservice.DTOs;
using OrderMicroservice.Models;

namespace OrderMicroservice.Interfaces
{
    public interface IOrderService
    {
        public OrderDto Create(OrderDto orderDto);
        public List<OrderDto> GetAll();
    }
}
