using BookService.Application.Common.Exceptions;
using BookService.Domain.Interfaces;
using MediatR;

namespace BookService.Application.Commands
{
    public record MarkBookAsRentedCommand(int BookId) : IRequest;

    public class MarkBookAsRentedCommandHandler : IRequestHandler<MarkBookAsRentedCommand>
    {
        private readonly IBookRepository _bookRepository;

        public MarkBookAsRentedCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task Handle(MarkBookAsRentedCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.BookId);
            if (book == null)
            {
                throw new NotFoundException("Book", request.BookId);
            }

            book.IsAccess = false;
            await _bookRepository.UpdateAsync(book);
        }
    }
}
