using MediatR;
using RentService.Domain.Entities;
using RentService.Domain.Interfaces;

namespace RentService.Application.Queries
{
    public record GetStatusByIdQuery(int Id) : IRequest<Status>;
    public class GetStatusByIdQueryHandler : IRequestHandler<GetStatusByIdQuery, Status>
    {
        private readonly IStatusRepository _repository;

        public GetStatusByIdQueryHandler(IStatusRepository repository)
        {
            _repository = repository;
        }

        public async Task<Status> Handle(GetStatusByIdQuery request, CancellationToken cancellationToken)
        {
            var status = await _repository.GetByIdAsync(request.Id);
            return status;
        }
    }
}