using OrderMicroservice.DTOs;

namespace OrderMicroservice.Interfaces
{
    public interface IOrderService
    {
        public Task<OrderDto> Create(OrderDto orderDto);
        public List<OrderDto> GetAll();
    }
}
