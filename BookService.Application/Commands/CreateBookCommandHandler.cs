using MediatR;
using BookService.Domain.Interfaces;
using BookService.Domain.Entities;
using BookService.Application.DTO;

namespace BookService.Application.Commands
{
    // Команда теперь возвращает BookDto вместо Book
    public record CreateBookCommand(
        string Title,
        List<int> AuthorIds,
        List<int> GenreIds,
        int? PublicationYear,
        string Description,
        bool IsAccess,
        string Condition) : IRequest<BookDto>;

    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, BookDto>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;

        public CreateBookCommandHandler(
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            IGenreRepository genreRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
        }

        public async Task<BookDto> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var authors = await _authorRepository.GetByIdsAsync(request.AuthorIds);
            var genres = await _genreRepository.GetByIdsAsync(request.GenreIds);

            var book = new Book
            {
                Title = request.Title,
                Authors = authors.ToList(),
                Genres = genres.ToList(),
                PublicationYear = request.PublicationYear,
                Description = request.Description,
                IsAccess = request.IsAccess,
                Condition = request.Condition
            };

            await _bookRepository.AddAsync(book);

            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                PublicationYear = book.PublicationYear,
                Description = book.Description,
                IsAccess = book.IsAccess,
                Condition = book.Condition,
                Authors = book.Authors.Select(a => new AuthorDto
                {
                    Id = a.Id,
                    FullName = a.FullName
                }).ToList(),
                Genres = book.Genres.Select(g => new GenreDto
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList()
            };
        }
    }
}