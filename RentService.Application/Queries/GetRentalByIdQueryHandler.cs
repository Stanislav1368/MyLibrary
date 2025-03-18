using MediatR;
using RentService.Domain.Entities;
using RentService.Domain.Interfaces;

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
