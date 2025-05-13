using MediatR;
using BookService.Domain.Interfaces;
using BookService.Domain.Entities;
using BookService.Application.Common.Exceptions;



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
                throw new BadRequestException("Неверный ID книги. ID не может быть <= 0");
            }

            var book = await _bookRepository.GetByIdAsync(request.Id);
            if (book == null)
            {
                throw new NotFoundException("Book", request.Id);
            }
            
            return book;         
        }
    }
}