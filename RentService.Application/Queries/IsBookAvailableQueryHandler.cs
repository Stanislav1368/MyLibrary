using MediatR;
using RentService.Domain.Interfaces;

namespace RentService.Application.Queries
{


    public record IsBookAvailableQuery(int BookId) : IRequest<bool>;

    public class IsBookAvailableQueryHandler : IRequestHandler<IsBookAvailableQuery, bool>
    {
        private readonly IRentalRepository _rentalRepository;

        public IsBookAvailableQueryHandler(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public async Task<bool> Handle(IsBookAvailableQuery request, CancellationToken cancellationToken)
        {
            return await _rentalRepository.IsBookRentedAsync(request.BookId);         
        }
    }
}
