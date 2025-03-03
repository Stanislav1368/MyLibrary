using MediatR;
using Microsoft.AspNetCore.Mvc;
using BookService.Application.Commands;
using BookService.Domain.Entities;

namespace BookService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GenresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Genre>> CreateGenre(CreateGenreCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
