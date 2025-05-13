using MediatR;
using RentService.Domain.Entities;
using RentService.Domain.Interfaces;



namespace RentService.Application.Commands
{
    /// <summary>
    /// Команда для регистрации нового библиотекаря в системе.
    /// </summary>
    public record RegisterLibrarianCommand(string Login, string Password, string FullName, string Email, string Phone) : IRequest;
    public class RegisterLibrarianCommandHandler : IRequestHandler<RegisterLibrarianCommand>
    {
        private readonly ILibrarianRepository _librarianRepository;
        private readonly IPasswordHasher _passwordHasher;
        public RegisterLibrarianCommandHandler(ILibrarianRepository librarianRepository, IPasswordHasher passwordHasher)
        {
            _librarianRepository = librarianRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task Handle(RegisterLibrarianCommand request, CancellationToken cancellationToken)
        {
            var librarian = new Librarian
            {
                Login = request.Login,
                PasswordHash = _passwordHasher.Generate(request.Password),
                FullName = request.FullName,
                Email = request.Email,
                Phone = request.Phone
            };

            await _librarianRepository.AddAsync(librarian);
        }
    }
}
