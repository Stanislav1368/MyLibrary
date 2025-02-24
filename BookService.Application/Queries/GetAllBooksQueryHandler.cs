using MediatR;
using BookService.Domain.Interfaces;
using BookService.Domain.Entities;


namespace BookService.Application.Queries
{
    public record GetAllBooksQuery : IRequest<List<Book>>;
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, List<Book>>
    {
        private readonly IBookRepository _bookRepository;

        public GetAllBooksQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<List<Book>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            return (List<Book>)await _bookRepository.GetAllAsync();
        }
    }
}