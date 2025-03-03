using MediatR;
using BookService.Domain.Entities;
using BookService.Domain.Interfaces;

namespace BookService.Application.Commands
{
    public record CreateRenterCommand(string FullName, string Address, string Phone, string Email) : IRequest<Renter>;
    public class CreateRenterCommandHandler : IRequestHandler<CreateRenterCommand, Renter>
    {
        private readonly IRenterRepository _renterRepository;
        public CreateRenterCommandHandler(IRenterRepository renterRepository)
        {
            _renterRepository = renterRepository;
        }

        public async Task<Renter> Handle(CreateRenterCommand request, CancellationToken cancellationToken)
        {
            var renter = new Renter
            {
                FullName = request.FullName,
                Address = request.Address,
                Phone = request.Phone,
                Email = request.Email
            };

            await _renterRepository.AddAsync(renter);
            return renter;
        }
    }
}
