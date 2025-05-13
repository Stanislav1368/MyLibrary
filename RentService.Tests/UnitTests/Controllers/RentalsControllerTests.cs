using RentService.API.Controllers;
using RentService.Application.Commands;
using RentService.Domain.Entities;
using MediatR;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using RentService.Application.Common.Exceptions;

namespace RentService.Tests.UnitTests.Controllers
{
    public class RentalsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly RentalsController _controller;

        public RentalsControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new RentalsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task CreateRental_ValidCommand_ReturnsOkWithRental()
        {
            // Arrange
            var command = new CreateRentalCommand(1, 2, 3, DateTime.Now, DateTime.Now.AddDays(7));
            var rental = new Rental { Id = 1 };

            _mediatorMock.Setup(x => x.Send(command, default))
                .ReturnsAsync(rental);

            // Act
            var result = await _controller.CreateRental(command);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(rental, okResult.Value);
        }

        [Fact]
        public async Task CreateRental_MediatorThrowsException_ReturnsAppropriateStatusCode()
        {
            // Arrange
            var command = new CreateRentalCommand(1, 2, 3, DateTime.Now, DateTime.Now.AddDays(7));

            _mediatorMock.Setup(x => x.Send(command, default))
                .ThrowsAsync(new NotFoundException("Renter", 2));

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _controller.CreateRental(command));
        }

    }
}