using MediatR;
using RentService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentService.Application.Commands
{
    public record UpdateRentalCommand(int Id, int StatusId, string Review) : IRequest;
    public class UpdateRentalCommandHandler : IRequestHandler<UpdateRentalCommand>
    {
        private readonly IRentalRepository _rentalRepository;
        public UpdateRentalCommandHandler(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }
        public async Task Handle(UpdateRentalCommand request, CancellationToken cancellationToken)
        {
            var rental = await _rentalRepository.GetByIdAsync(request.Id);
            if (rental == null)
            {
                throw new Exception("Аренда с таким Id не найдена");
            }

            rental.StatusId = request.StatusId;
            rental.Review = request.Review;
            await _rentalRepository.UpdateAsync(rental);
        }
    }
}
