using Microsoft.AspNetCore.Mvc;
using OrderMicroservice.DTOs;
using OrderMicroservice.Interfaces;

namespace OrderMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService) => _orderService = orderService;

        [HttpGet]
        [Route("GetAllOrders")]
        public ActionResult<IEnumerable<OrderDto>> Get()
        {
            var orders = _orderService.GetAll();
            return Ok(orders);
        }
        [HttpGet]
        [Route("create")]
        public ActionResult<OrderDto> Create(int amount, int customerId, DateTime deliveryDate)
        {
            var order = _orderService.Create(new OrderDto
            {
                Amount = amount,
                CustomerId = customerId,
                DeliveryDate = deliveryDate
            });
            return Ok(order);
        }
    }
}
