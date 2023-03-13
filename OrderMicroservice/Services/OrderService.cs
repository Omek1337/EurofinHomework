using OrderMicroservice.DTOs;
using OrderMicroservice.Interfaces;
using OrderMicroservice.Models;

namespace OrderMicroservice.Services
{

    public class OrderService : IOrderService
    {
        private List<Order> _ordersList => new();
        private decimal _price = 98.99M;
        private int idCounter = 1;
        public OrderDto Create(OrderDto orderDto)
        {
            var amount = CheckAmount(orderDto.Amount) ? orderDto.Amount : 0;
            var cost = amount >= 10 ? 
                SetDiscount(amount, CountTotalCost(amount)) : 
                CountTotalCost(amount);

            _ordersList.Add(new Order
            {
                Id = idCounter++,
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
            var orders = _ordersList.OrderBy(x => x.Id).ToList();
            return MapOrdersToDtos(orders);
        }
        private bool CheckAmount(int amount)
        {
            if (amount < 0 || amount > 999)
            {
                return false;
            }
            return true;
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
