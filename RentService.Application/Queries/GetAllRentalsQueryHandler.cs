using MediatR;
using BookService.Domain.Interfaces;
using BookService.Domain.Entities;
using BookService.Domain.Entities;
using BookService.Domain.Interfaces;


namespace BookService.Application.Queries
{
    public record GetAllRentalsQuery() : IRequest<IEnumerable<Rental>>;
    public class GetAllRentalsQueryHandler : IRequestHandler<GetAllRentalsQuery, IEnumerable<Rental>>
    {
        private readonly IRentalRepository _rentalRepository;
        public GetAllRentalsQueryHandler(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }
        public async Task<IEnumerable<Rental>> Handle(GetAllRentalsQuery request, CancellationToken cancellationToken)
        {
            return await _rentalRepository.GetAllAsync();
        }
    }
}