using MediatR;
using Microsoft.AspNetCore.Http;
using RentService.Domain.Entities;
using RentService.Domain.Interfaces;



namespace RentService.Application.Commands
{
    public record LoginLibrarianCommand(string Email, string Password) : IRequest<string>;
    public class LoginLibrarianCommandHandler : IRequestHandler<LoginLibrarianCommand, string>
    {
        private readonly ILibrarianRepository _librarianRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenProvider _tokenProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoginLibrarianCommandHandler(ILibrarianRepository librarianRepository, IPasswordHasher passwordHasher, ITokenProvider tokenProvider, IHttpContextAccessor httpContextAccessor)
        {
            _librarianRepository = librarianRepository;
            _passwordHasher = passwordHasher;
            _tokenProvider = tokenProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> Handle(LoginLibrarianCommand request, CancellationToken cancellationToken)
        {
            var librarian = await _librarianRepository.GetByEmailAsync(request.Email);
            if (librarian == null)
            {
                throw new Exception("Пользователь не найден");
            }

            var result = _passwordHasher.Verify(request.Password, librarian.PasswordHash);
            if (!result)
            {
                throw new UnauthorizedAccessException("Неверный пароль");
            }

            var token = _tokenProvider.GenerateToken(librarian);

            var httpContext = _httpContextAccessor.HttpContext;
            httpContext?.Response.Cookies.Append("cookies_", token);

            return token;
        }
    }
}
