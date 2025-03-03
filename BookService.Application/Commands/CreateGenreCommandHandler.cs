using BookService.Application.Common;
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
    public record CreateGenreCommand(string Name) : IRequest<Genre>;
    public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, Genre>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly ValidatorService _validatorService;

        public CreateGenreCommandHandler(IGenreRepository genreRepository, ValidatorService validatorService)
        {
            _genreRepository = genreRepository;
            _validatorService = validatorService;
        }

        public async Task<Genre> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            await _validatorService.ValidateAsync<CreateGenreCommand>(request, cancellationToken);

            var genre = new Genre
            {
                Name = request.Name
            };

            await _genreRepository.AddAsync(genre);
            return genre;
        }
    }
}
