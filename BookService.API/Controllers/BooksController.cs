using MediatR;
using Microsoft.AspNetCore.Mvc;
using BookService.Application.Commands;
using BookService.Domain.Entities;
using BookService.Application.Queries;

namespace BookService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(CreateBookCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetAllBooks()
        {
            var result = await _mediator.Send(new GetAllBooksQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var result = await _mediator.Send(new GetBookByIdQuery(id));
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(int id, UpdateBookCommand command)
        {
            await _mediator.Send(new UpdateBookCommand(id, command.Title, command.AuthorIds, command.GenreIds, command.PublicationYear, command.Description, command.IsAccess, command.Condition));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            await _mediator.Send(new DeleteBookCommand(id));
            return NoContent();
        }
        [HttpGet("search")]
        public async Task<ActionResult<List<Book>>> SearchBooks(SearchBooksQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}