using MediatR;
using BookService.Domain.Entities;
using BookService.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace BookService.Application.Queries
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