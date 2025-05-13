using MassTransit;
using MediatR;
using RentService.Application.Common.Exceptions;
using RentService.Domain.Entities;
using RentService.Domain.Interfaces;
using SharedContracts;

namespace RentService.Application.Commands
{
    public record CloseRentalCommand(int RentalId, DateTime ActualReturnDate, string? Review = null) : IRequest;

    public class CloseRentalCommandHandler : IRequestHandler<CloseRentalCommand>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public CloseRentalCommandHandler(IRentalRepository rentalRepository, IPublishEndpoint publishEndpoint)
        {
            _rentalRepository = rentalRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(CloseRentalCommand request, CancellationToken cancellationToken)
        {
          
            var rental = await _rentalRepository.GetByIdAsync(request.RentalId) ?? throw new NotFoundException("Rental", request.RentalId);
            if (rental.StatusId == (int)StatusType.Completed)
            {
                throw new ConflictException("Аренда уже закрыта");
            }
            rental.ActualReturnDate = request.ActualReturnDate;
            rental.Review = request.Review;
            await _rentalRepository.UpdateAsync(rental);
 
            await _publishEndpoint.Publish(
                new BookReturnedEvent(rental.BookId),
                cancellationToken
            );
        }
    }
}
