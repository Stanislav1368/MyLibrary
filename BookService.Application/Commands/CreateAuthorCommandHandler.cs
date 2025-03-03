using BookService.Domain.Entities;
using BookService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using BookService.Application.Common;

namespace BookService.Application.Commands
{
    public record CreateAuthorCommand(
       string FullName,
       DateTime? BirthDate,
       string Country,
       string Biography) : IRequest<Author>;
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Author>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ValidatorService _validatorService;
        public CreateAuthorCommandHandler(IAuthorRepository authorRepository, ValidatorService validatorService)
        {
            _authorRepository = authorRepository;
            _validatorService = validatorService;
        }

        public async Task<Author> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            await _validatorService.ValidateAsync<CreateAuthorCommand>(request, cancellationToken);

            var author = new Author
            {
                FullName = request.FullName,
                BirthDate = request.BirthDate?.ToUniversalTime(),
                Country = request.Country,
                Biography = request.Biography
            };

            await _authorRepository.AddAsync(author);
            return author;
        }
    }
}
