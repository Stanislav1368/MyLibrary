using MediatR;
using BookService.Domain.Interfaces;
using BookService.Domain.Entities;



namespace BookService.Application.Queries
{
    public record GetBookByIdQuery(int Id) : IRequest<Book>;

    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookByIdQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
            {
                throw new ArgumentException("Неверный ID книги.");
            }

            var book = await _bookRepository.GetByIdAsync(request.Id);
            if (book == null)
            {
                throw new Exception("Книга с данным ID не найдена.");
            }
            
            return book;         
        }
    }
}