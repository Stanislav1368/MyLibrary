using MediatR;
using BookService.Domain.Entities;
using BookService.Domain.Interfaces;



namespace BookService.Application.Commands
{
    public record CreateLibrarianCommand(string Login, string PasswordHash, string FullName, string Email, string Phone) : IRequest<Librarian>;
    public class CreateLibrarianCommandHandler : IRequestHandler<CreateLibrarianCommand, Librarian>
    {
        private readonly ILibrarianRepository _librarianRepository;
        public CreateLibrarianCommandHandler(ILibrarianRepository librarianRepository)
        {
            _librarianRepository = librarianRepository;
        }

        public async Task<Librarian> Handle(CreateLibrarianCommand request, CancellationToken cancellationToken)
        {
            var librarian = new Librarian
            {
                Login = request.Login,
                PasswordHash = request.PasswordHash,
                FullName = request.FullName,
                Email = request.Email,
                Phone = request.Phone
            };

            await _librarianRepository.AddAsync(librarian);
            return librarian;
        }
    }
}
