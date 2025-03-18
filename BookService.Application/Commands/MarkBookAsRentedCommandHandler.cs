using BookService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                throw new Exception("Книга не найдена");
            }

            book.IsAccess = false; // Помечаем книгу как недоступную
            await _bookRepository.UpdateAsync(book);
        }
    }
}
