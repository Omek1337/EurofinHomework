using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OrderMicroservice.Controllers;
using OrderMicroservice.Interfaces;
using OrderMicroservice.Services;

namespace OrderMicroserviceTests
{
    public class ListOrderTest
    {
        [Fact]
        public void GetAllOrders()
        {
            // arrange
            var repositoryMock = new Mock<IOrderRepository>();
            var orderService = new OrderService(repositoryMock.Object);
            var controller = new OrderController(orderService);
            // act
            var order1 = controller.Create(
                amount: 1,
                customerId: 1,
                deliveryDate: DateTime.UtcNow.AddDays(1));
            var order2 = controller.Create(
                amount: 10,
                customerId: 2,
                deliveryDate: DateTime.UtcNow.AddDays(2));
            var order3 = controller.Create(
                amount: 50,
                customerId: 3,
                deliveryDate: DateTime.UtcNow.AddDays(3));

            var resultList = controller.Get();
            var result = resultList.Result as OkObjectResult;
            // assert
            Assert.NotNull(order1);
            Assert.NotNull(order2);
            Assert.NotNull(order3);

            Assert.NotNull(resultList);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }
    }
}
