using MediatR;
using BookService.Domain.Interfaces;
using BookService.Domain.Entities;


namespace BookService.Application.Queries
{
    public record GetAllLibrariansQuery() : IRequest<IEnumerable<Librarian>>;
    public class GetAllLibrariansQueryHandler : IRequestHandler<GetAllLibrariansQuery, IEnumerable<Librarian>>
    {
        private readonly ILibrarianRepository _librarianRepository;
        public GetAllLibrariansQueryHandler(ILibrarianRepository librarianRepository)
        {
            _librarianRepository = librarianRepository;
        }
        public async Task<IEnumerable<Librarian>> Handle(GetAllLibrariansQuery request, CancellationToken cancellationToken)
        {
            return await _librarianRepository.GetAllAsync();
        }
    }
}