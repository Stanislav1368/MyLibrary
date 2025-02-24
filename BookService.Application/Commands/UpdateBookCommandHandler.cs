﻿using BookService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookService.Application.Commands
{
    public record UpdateBookCommand(
    int Id,
    string Title,
    List<int> AuthorIds,
    List<int> GenreIds,
    int? PublicationYear,
    string Description,
    bool IsAccess,
    string Condition) : IRequest;
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;

        public UpdateBookCommandHandler(
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            IGenreRepository genreRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
        }

        public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.Id);
            if (book == null)
                throw new Exception("Book not found");

            var authors = await _authorRepository.GetByIdsAsync(request.AuthorIds);
            var genres = await _genreRepository.GetByIdsAsync(request.GenreIds);

            book.Title = request.Title;
            book.Authors = authors.ToList();
            book.Genres = genres.ToList();
            book.PublicationYear = request.PublicationYear;
            book.Description = request.Description;
            book.IsAccess = request.IsAccess;
            book.Condition = request.Condition;

            await _bookRepository.UpdateAsync(book);
        }
    }
}
