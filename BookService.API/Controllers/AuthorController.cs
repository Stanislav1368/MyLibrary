using BookService.Application.Commands;
using BookService.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Author>> CreateAuthor(CreateAuthorCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
