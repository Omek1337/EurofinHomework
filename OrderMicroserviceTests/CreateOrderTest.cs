using Moq;
using OrderMicroservice.DTOs;
using OrderMicroservice.Exceptions;
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
            // act
            // assert
        }

        [Fact]
        public void NotifiesOfFailedOrder()
        {
            // arrange
            // act
            // assert
        }

        [Fact]
        public void RejectWhenDateIsNotInTheFuture()
        {
            // arrange
            var repositoryMock = new Mock<IOrderRepository>();
            var orderService = new OrderService(repositoryMock.Object);

            // assert
            Assert.ThrowsAsync<DateNotRightException>(async () => await orderService.Create(new OrderDto
            {
                Amount = 1,
                CustomerId = 1,
                DeliveryDate = DateTime.UtcNow.AddDays(-1),
            }));
        }

        [Fact]
        public void RejectWhenAmountIsNegative()
        {
            // arrange
            var repositoryMock = new Mock<IOrderRepository>();
            var orderService = new OrderService(repositoryMock.Object);

            // assert
            Assert.ThrowsAsync<AmountNotRightException>(async () => await orderService.Create(new OrderDto
            {
                Amount = -1,
                CustomerId = 1,
                DeliveryDate = DateTime.UtcNow.AddDays(1),
            }));
        }

        [Fact]
        public void RejectWhenAmountIsMoreThan999()
        {
            // arrange
            var repositoryMock = new Mock<IOrderRepository>();
            var orderService = new OrderService(repositoryMock.Object);

            // assert
            Assert.ThrowsAsync<AmountNotRightException>(async () => await orderService.Create(new OrderDto
            {
                Amount = 1001,
                CustomerId = 1,
                DeliveryDate = DateTime.UtcNow.AddDays(1),
            }));
        }

        [Fact]
        public void GetOrderDiscount()
        {
            // arrange
            // act
            // assert
        }
    }
}