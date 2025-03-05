using MediatR;
using RentService.Domain.Interfaces;
using RentService.Domain.Entities;


namespace RentService.Application.Queries
{
    public record GetAvailableBooksQuery() : IRequest<IEnumerable<Librarian>>;
    public class GetAvailableBooksQueryHandler : IRequestHandler<GetAvailableBooksQuery, IEnumerable<Librarian>>
    {
        private readonly ILibrarianRepository _librarianRepository;
        public GetAvailableBooksQueryHandler(ILibrarianRepository librarianRepository)
        {
            _librarianRepository = librarianRepository;
        }
        public async Task<IEnumerable<Librarian>> Handle(GetAvailableBooksQuery request, CancellationToken cancellationToken)
        {
            return await _librarianRepository.GetAllAsync();
        }
    }
}