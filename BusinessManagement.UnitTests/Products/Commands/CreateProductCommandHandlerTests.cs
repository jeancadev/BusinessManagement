using BusinessManagement.Application.Common.Interfaces;
using BusinessManagement.Application.Products.Commands;
using BusinessManagement.Domain.Entities;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BusinessManagement.UnitTests.Products.Commands
{
    public class CreateProductCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Create_Product_And_Return_Id()
        {
            // Arrange
            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(r => r.AddAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

            var handler = new CreateProductCommandHandler(mockProductRepo.Object);

            var command = new CreateProductCommand(
                Name: "Laptop",
                Description: "Gaming Laptop",
                Price: 1500m,
                Stock: 10
            );

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.NotEqual(Guid.Empty, result);
            mockProductRepo.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
        }
    }
}
