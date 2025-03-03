using MediatR;
using Microsoft.AspNetCore.Mvc;
using BookService.Application.Commands;
using BookService.Application.Queries;
using BookService.Domain.Entities;

namespace BookService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibrarianController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LibrarianController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Librarian>> CreateLibrarian(CreateLibrarianCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<Librarian>>> GetAllLibrarians()
        {
            var result = await _mediator.Send(new GetAllLibrariansQuery());
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLibrarian(int id)
        {
            await _mediator.Send(new DeleteLibrarianCommand(id));
            return NoContent();
        }
    }
}