using MediatR;
using RentService.Domain.Interfaces;

namespace RentService.Application.Commands
{
    public record UpdateRenterCommand(int Id, string FullName, string Email, string Phone) : IRequest;
    public class UpdateRenterCommandHandler : IRequestHandler<UpdateRenterCommand>
    {
        private readonly IRenterRepository _renterRepository;
        public UpdateRenterCommandHandler(IRenterRepository renterRepository)
        {
            _renterRepository = renterRepository;
        }
        public async Task Handle(UpdateRenterCommand request, CancellationToken cancellationToken)
        {
            var renter = await _renterRepository.GetByIdAsync(request.Id);
            if (renter == null) throw new Exception("Арендатор с таким Id не найден");

            renter.FullName = request.FullName;
            renter.Email = request.Email;
            renter.Phone = request.Phone;

            await _renterRepository.UpdateAsync(renter);
        }
    }

}
