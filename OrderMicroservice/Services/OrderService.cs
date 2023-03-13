using DataContext.Models;
using OrderMicroservice.DTOs;
using OrderMicroservice.Interfaces;
using OrderMicroservice.Repositories;

namespace OrderMicroservice.Services
{

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        private decimal _price = 98.99M;
        public async Task<OrderDto> Create(OrderDto orderDto)
        {
            var amount = CheckAmount(orderDto.Amount) ? orderDto.Amount : 0;
            var cost = amount >= 10 ? 
                SetDiscount(amount, CountTotalCost(amount)) : 
                CountTotalCost(amount);
            if (!CheckDate(orderDto.DeliveryDate)) throw new Exception("Date cannot be less than NOW");
            await _orderRepository.Create(new Order
            {
                Id = Guid.NewGuid(),
                Amount = orderDto.Amount,
                Cost = Math.Round(cost, 2, MidpointRounding.AwayFromZero),
                CustomerId = orderDto.CustomerId,
                DeliveryDate = orderDto.DeliveryDate
            });
            orderDto.Cost = Math.Round(cost, 2, MidpointRounding.AwayFromZero);
            return orderDto;
        }

        public List<OrderDto> GetAll()
        {
            var orders = _orderRepository.GetAll();
            return MapOrdersToDtos(orders);
        }
        private bool CheckAmount(int amount)
        {
            if (amount < 0 || amount >= 999)
            {
                throw new Exception("Amount should be from 1 to 999");
            }
            return true;
        }

        private bool CheckDate(DateTime date)
        {
            
            return DateTime.UtcNow <= date ? true : false;
        }
        private decimal CountTotalCost(int amount) => amount * _price;

        private decimal SetDiscount(int amount, decimal totalCost)
        {
            var discountCost = amount < 50 ? totalCost - (totalCost * 0.05M) :
                totalCost - (totalCost * 0.15M);
            return discountCost;
        }

        private List<OrderDto> MapOrdersToDtos(List<Order> orders)
        {
            var orderDtoList = new List<OrderDto>();
            foreach (var item in orders)
            {
                orderDtoList.Add(new OrderDto
                {
                    Amount = item.Amount,
                    Cost = item.Cost,
                    CustomerId = item.CustomerId,
                    DeliveryDate = item.DeliveryDate
                });
            }
            return orderDtoList;
        }
    }
}
