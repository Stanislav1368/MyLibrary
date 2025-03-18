using MediatR;
using RentService.Domain.Interfaces;

namespace RentService.Application.Commands
{
    public record DeleteRenterCommand(int Id) : IRequest;
    public class DeleteRenterCommandHandler : IRequestHandler<DeleteRenterCommand>
    {
        private readonly IRenterRepository _renterRepository;
        public DeleteRenterCommandHandler(IRenterRepository renterRepository)
        {
            _renterRepository = renterRepository;
        }
        public async Task Handle(DeleteRenterCommand request, CancellationToken cancellationToken)
        {
            await _renterRepository.DeleteAsync(request.Id);
        }
    }
}
