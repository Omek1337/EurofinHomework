using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderMicroservice.DTOs;

namespace OrderMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        [HttpGet]
        [Route("GetAllOrders")]
        public ActionResult<IEnumerable<OrderDto>> Get()
        {
            return Ok();
        }

        public ActionResult Create()
        {
            return Ok();
        }
    }
}
