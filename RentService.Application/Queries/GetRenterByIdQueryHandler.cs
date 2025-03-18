using MediatR;
using RentService.Domain.Entities;
using RentService.Domain.Interfaces;

namespace RentService.Application.Queries
{
    public record GetRenterByIdQuery(int Id) : IRequest<Renter>;
    public class GetRenterByIdQueryHandler : IRequestHandler<GetRenterByIdQuery, Renter>
    {
        private readonly IRenterRepository _repository;

        public GetRenterByIdQueryHandler(IRenterRepository repository)
        {
            _repository = repository;
        }

        public async Task<Renter> Handle(GetRenterByIdQuery request, CancellationToken cancellationToken)
        {
            var renter = await _repository.GetByIdAsync(request.Id);
            return renter;
        }
    }
}