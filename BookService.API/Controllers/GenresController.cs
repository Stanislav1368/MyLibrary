using BookService.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<int>> CreateGenre(CreateGenreCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
