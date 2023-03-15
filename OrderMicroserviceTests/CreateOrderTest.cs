using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OrderMicroservice.Controllers;
using OrderMicroservice.Interfaces;
using OrderMicroservice.Services;

namespace OrderMicroserviceTests
{
    public class CreateOrderTest
    {
        [Fact]
        public void NotifiesOfSuccessfulOrder()
        {
            // arrange
            var repositoryMock = new Mock<IOrderRepository>();
            var orderService = new OrderService(repositoryMock.Object);
            var controller = new OrderController(orderService);
            // act
            var result = controller.Create(
                amount: 1,
                customerId: 1,
                deliveryDate: DateTime.UtcNow.AddDays(1)).Result as OkObjectResult;
            // assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public void NotifiesOfFailedOrder()
        {
            // arrange
            var repositoryMock = new Mock<IOrderRepository>();
            var orderService = new OrderService(repositoryMock.Object);
            var controller = new OrderController(orderService);
            // act
            var result = controller.Create(
                amount: -1,
                customerId: -1,
                deliveryDate: DateTime.UtcNow.AddDays(1));
            // assert
            var statuscode = result.Result as BadRequestObjectResult;
            Assert.Null(result.Value);
            Assert.Equal(StatusCodes.Status400BadRequest, statuscode.StatusCode);
        }

        [Fact]
        public void RejectWhenDateIsNotInTheFuture()
        {
            // arrange
            var repositoryMock = new Mock<IOrderRepository>();
            var orderService = new OrderService(repositoryMock.Object);
            var controller = new OrderController(orderService);
            // act
            var result = controller.Create(
                amount: 1,
                customerId: 1,
                deliveryDate: DateTime.UtcNow.AddDays(-2));
            // assert
            var statuscode = result.Result as BadRequestObjectResult;
            Assert.Null(result.Value);
            Assert.Equal(StatusCodes.Status400BadRequest, statuscode.StatusCode);
        }

        [Fact]
        public void RejectWhenAmountIsNegative()
        {
            // arrange
            var repositoryMock = new Mock<IOrderRepository>();
            var orderService = new OrderService(repositoryMock.Object);
            var controller = new OrderController(orderService);
            // act
            var result = controller.Create(
                amount: -11,
                customerId: 1,
                deliveryDate: DateTime.UtcNow.AddDays(2));
            // assert
            var statuscode = result.Result as BadRequestObjectResult;
            Assert.Null(result.Value);
            Assert.Equal(StatusCodes.Status400BadRequest, statuscode.StatusCode);
        }

        [Fact]
        public void RejectWhenAmountIsMoreThan999()
        {
            // arrange
            var repositoryMock = new Mock<IOrderRepository>();
            var orderService = new OrderService(repositoryMock.Object);
            var controller = new OrderController(orderService);
            // act
            var result = controller.Create(
                amount: 1001,
                customerId: 1,
                deliveryDate: DateTime.UtcNow.AddDays(2));
            // assert
            var statuscode = result.Result as BadRequestObjectResult;
            Assert.Null(result.Value);
            Assert.Equal(StatusCodes.Status400BadRequest, statuscode.StatusCode);
        }
    }
}