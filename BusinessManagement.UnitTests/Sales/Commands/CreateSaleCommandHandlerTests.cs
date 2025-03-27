using BusinessManagement.Application.Common.Interfaces;
using BusinessManagement.Application.Sales.Commands;
using BusinessManagement.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BusinessManagement.UnitTests.Sales.Commands
{
    public class CreateSaleCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Create_Sale_And_Return_Id()
        {
            // Arrange
            var mockSaleRepo = new Mock<ISaleRepository>();
            var mockInventoryRepo = new Mock<IInventoryRepository>();
            mockSaleRepo.Setup(r => r.AddAsync(It.IsAny<Sale>()))
                    .Returns(Task.CompletedTask);

            var handler = new CreateSaleCommandHandler(mockSaleRepo.Object, mockInventoryRepo.Object);

            var command = new CreateSaleCommand(
                CustomerId: Guid.NewGuid(),
                Items: new List<Application.Sales.DTOs.SaleItemDto>
                {
                        new Application.Sales.DTOs.SaleItemDto
                        {
                            ProductId = Guid.NewGuid(),
                            Quantity = 2,
                            UnitPrice = 10
                        }
                }
            );

            // Act
            var result = await handler.Handle(command, default);

            // Assert
            Assert.NotEqual(Guid.Empty, result); // Debe retornar un ID válido
            mockSaleRepo.Verify(r => r.AddAsync(It.IsAny<Sale>()), Times.Once);
        }
    }
}
