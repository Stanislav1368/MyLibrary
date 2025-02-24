using MediatR;
using BookService.Domain.Interfaces;
using BookService.Domain.Entities;
using BookService.Application.DTO;


namespace BookService.Application.Queries
{
    public record GetBookByIdQuery(int Id) : IRequest<BookDto>;

    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDto>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookByIdQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.Id);

            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                PublicationYear = book.PublicationYear,
                Description = book.Description,
                Genres = book.Genres.Select(g => new GenreDto { Id = g.Id, Name = g.Name }).ToList(),
                Authors = book.Authors.Select(a => new AuthorDto { Id = a.Id, FullName = a.FullName }).ToList(),
                Condition = book.Condition
            };
        }
    }
}