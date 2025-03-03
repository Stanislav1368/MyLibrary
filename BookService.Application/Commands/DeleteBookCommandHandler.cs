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
                throw new ArgumentException("Неверный ID книги.");
            }

            var book = await _bookRepository.GetByIdAsync(request.Id);
            if (book == null)
            {
                throw new Exception("Книга с данным ID не найдена.");
            }

            await _bookRepository.DeleteAsync(request.Id);
        }
    }
}
