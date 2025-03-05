using MediatR;
using RentService.Domain.Interfaces;
using RentService.Domain.Entities;



namespace RentService.Application.Queries
{
    public record GetAllRentersQuery() : IRequest<IEnumerable<Renter>>;
    public class GetAllRentersQueryHandler : IRequestHandler<GetAllRentersQuery, IEnumerable<Renter>>
    {
        private readonly IRenterRepository _renterRepository;
        public GetAllRentersQueryHandler(IRenterRepository renterRepository)
        {
            _renterRepository = renterRepository;
        }
        public async Task<IEnumerable<Renter>> Handle(GetAllRentersQuery request, CancellationToken cancellationToken)
        {
            return await _renterRepository.GetAllAsync();
        }
    }
}