using MediatR;
using BookService.Domain.Interfaces;
using BookService.Domain.Entities;


namespace BookService.Application.Queries
{
    public record GetAllBooksQuery : IRequest<IEnumerable<Book>>;
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<Book>>
    {
        private readonly IBookRepository _bookRepository;

        public GetAllBooksQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetAllAsync();
            return books;
        }
    }
}