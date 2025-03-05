using MediatR;
using RentService.Domain.Interfaces;

namespace RentService.Application.Commands
{
    public record DeleteRentalCommand(int Id) : IRequest;
    public class DeleteRentalCommandHandler : IRequestHandler<DeleteRentalCommand>
    {
        private readonly IRentalRepository _rentalRepository;
        public DeleteRentalCommandHandler(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }
        public async Task Handle(DeleteRentalCommand request, CancellationToken cancellationToken)
        {
            await _rentalRepository.DeleteAsync(request.Id);
        }
    }
}
