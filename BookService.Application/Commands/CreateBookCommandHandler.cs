using MediatR;
using BookService.Domain.Interfaces;
using BookService.Domain.Entities;
using FluentValidation;
using BookService.Application.Common;
using BookService.Application.Common.Exceptions;

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
            if (authors.Count() != request.AuthorIds.Count)
                throw new NotFoundException("Author", request.AuthorIds);

            var genres = await _genreRepository.GetByIdsAsync(request.GenreIds);
            if (genres.Count() != request.GenreIds.Count)
                throw new NotFoundException("Genre", request.GenreIds);

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
        }
    }
}