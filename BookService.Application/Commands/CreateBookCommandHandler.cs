using MediatR;
using BookService.Domain.Interfaces;
using BookService.Domain.Entities;
using FluentValidation;
using BookService.Application.Common;

namespace BookService.Application.Commands
{
    public record CreateBookCommand(
        string Title,
        List<int> AuthorIds,
        List<int> GenreIds,
        int? PublicationYear,
        string Description,
        string Condition) : IRequest<Book>;

    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Book>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly ValidatorService _validatorService;
        public CreateBookCommandHandler(
             IBookRepository bookRepository,
             IAuthorRepository authorRepository,
             IGenreRepository genreRepository,
             ValidatorService validatorService)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
            _validatorService = validatorService;
        }

        public async Task<Book> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            await _validatorService.ValidateAsync<CreateBookCommand>(request, cancellationToken);

            var authors = await _authorRepository.GetByIdsAsync(request.AuthorIds);
            var genres = await _genreRepository.GetByIdsAsync(request.GenreIds);

            var book = new Book
            {
                Title = request.Title,
                Authors = authors.ToList(),
                Genres = genres.ToList(),
                PublicationYear = request.PublicationYear,
                Description = request.Description,
                Condition = request.Condition
            };

            await _bookRepository.AddAsync(book);
            return book;
            //return new BookDto
            //{
            //    Id = book.Id,
            //    Title = book.Title,
            //    PublicationYear = book.PublicationYear,
            //    Description = book.Description,
            //    IsAccess = book.IsAccess,
            //    Condition = book.Condition,
            //    Authors = book.Authors.Select(a => new AuthorDto
            //    {
            //        Id = a.Id,
            //        FullName = a.FullName
            //    }).ToList(),
            //    Genres = book.Genres.Select(g => new GenreDto
            //    {
            //        Id = g.Id,
            //        Name = g.Name
            //    }).ToList()
            //};
        }
    }
}