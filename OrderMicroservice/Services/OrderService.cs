using DataContext.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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

            var cost = orderDto.Amount >= 10 ?
                SetDiscount(orderDto.Amount, CountTotalCost(orderDto.Amount)) :
                CountTotalCost(orderDto.Amount);
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
            if (orders == null)
            {
                return new List<OrderDto>();
            }
            return MapOrdersToDtos(orders);
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
