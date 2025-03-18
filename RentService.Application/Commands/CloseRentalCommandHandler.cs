using MassTransit;
using MediatR;
using RentService.Domain.Interfaces;
using SharedContracts;

namespace RentService.Application.Commands
{
    public record CloseRentalCommand(int RentalId, DateTime ActualReturnDate) : IRequest;

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
            var rental = await _rentalRepository.GetByIdAsync(request.RentalId);
            if (rental == null)
            {
                throw new Exception("Аренда не найдена");
            }

            rental.ActualReturnDate = request.ActualReturnDate;
            await _rentalRepository.UpdateAsync(rental);
 
            await _publishEndpoint.Publish(
                new BookReturnedEvent(rental.BookId),
                cancellationToken
            );
        }
    }
}
