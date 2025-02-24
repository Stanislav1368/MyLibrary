using BookService.Domain.Entities;
using BookService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public CreateAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Author> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
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
