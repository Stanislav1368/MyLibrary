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
    public record DeleteBookCommand(int Id) : IRequest;
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
    {
        private readonly IBookRepository _bookRepository;

        public DeleteBookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
            {
                throw new BadRequestException("Неверный ID книги. ID не может быть <= 0");
            }

            var book = await _bookRepository.GetByIdAsync(request.Id);
            if (book == null)
            {
                throw new NotFoundException("Book", request.Id);
            }

            await _bookRepository.DeleteAsync(request.Id);
        }
    }
}
