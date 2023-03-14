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
            try
            {
                var orders = _orderService.GetAll();
                return Ok(orders);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }
        [HttpGet]
        [Route("create")]
        public ActionResult<OrderDto> Create(int amount, int customerId, DateTime deliveryDate)
        {
            try
            {
                if (CheckAmount(amount) && CheckDate(deliveryDate)) 
                {
                    var order = _orderService.Create(new OrderDto
                    {
                        Amount = amount,
                        CustomerId = customerId,
                        DeliveryDate = deliveryDate
                    }).Result;
                    return Ok(order);
                }
                return BadRequest("Wrong date or amount");
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }   
        }

        private bool CheckAmount(int amount)
        {
            if (amount < 0 || amount >= 999)
            {
                return false;
            }
            return true;
        }

        private bool CheckDate(DateTime date)
        {

            return DateTime.UtcNow <= date ? true : false;
        }
    }
}
