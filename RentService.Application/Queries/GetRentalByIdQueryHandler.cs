using MediatR;
using RentService.Domain.Entities;
using RentService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentService.Application.Queries
{
    public record GetRentalByIdQuery(int Id) : IRequest<Rental>;
    public class GetRentalByIdQueryHandler : IRequestHandler<GetRentalByIdQuery, Rental>
    {
        private readonly IRentalRepository _repository;

        public GetRentalByIdQueryHandler(IRentalRepository repository)
        {
            _repository = repository;
        }

        public async Task<Rental> Handle(GetRentalByIdQuery request, CancellationToken cancellationToken)
        {
            var rental = await _repository.GetByIdAsync(request.Id);
            return rental;
        }
    }
}
