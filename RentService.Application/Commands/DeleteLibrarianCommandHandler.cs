using MediatR;
using RentService.Domain.Interfaces;


namespace RentService.Application.Commands
{
    public record DeleteLibrarianCommand(int Id) : IRequest;
    public class DeleteLibrarianCommandHandler : IRequestHandler<DeleteLibrarianCommand>
    {
        private readonly ILibrarianRepository _librarianRepository;
        public DeleteLibrarianCommandHandler(ILibrarianRepository librarianRepository)
        {
            _librarianRepository = librarianRepository;
        }
        public async Task Handle(DeleteLibrarianCommand request, CancellationToken cancellationToken)
        {
            await _librarianRepository.DeleteAsync(request.Id);
        }
    }
}
