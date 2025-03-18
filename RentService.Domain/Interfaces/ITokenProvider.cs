using RentService.Domain.Entities;

namespace RentService.Domain.Interfaces

{
    public interface ITokenProvider
    {
        string GenerateToken(Librarian librarian);
    }
}