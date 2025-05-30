﻿using MediatR;
using RentService.Application.Common.Exceptions;
using RentService.Domain.Interfaces;

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
                throw new NotFoundException("Rental", request.Id);
            }

            rental.StatusId = request.StatusId;
            rental.Review = request.Review;
            await _rentalRepository.UpdateAsync(rental);
        }
    }
}
