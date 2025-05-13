using BookService.Application.Common.Exceptions;
using BookService.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookService.Application.Commands
{
    public record MarkBookAsAvailableCommand(int BookId) : IRequest;

    public class MarkBookAsAvailableCommandHandler : IRequestHandler<MarkBookAsAvailableCommand>
    {
        private readonly IBookRepository _bookRepository;

        public MarkBookAsAvailableCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task Handle(MarkBookAsAvailableCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.BookId);
            if (book == null)
            {
                throw new NotFoundException("Book", request.BookId);
            }

            book.IsAccess = true;
            await _bookRepository.UpdateAsync(book);
        }
    }
}
