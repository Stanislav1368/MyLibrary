using MediatR;
using Microsoft.AspNetCore.Mvc;
using BookService.Application.Commands;
using BookService.Application.Queries;
using BookService.Domain.Entities;


namespace BookService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RentalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Rental>> CreateRental(CreateRentalCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<Rental>>> GetAllRentals()
        {
            var result = await _mediator.Send(new GetAllRentalsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rental>> GetRental(int id)
        {
            var result = await _mediator.Send(new GetRentalByIdQuery(id));
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRental(int id, UpdateRentalCommand command)
        {

            await _mediator.Send(new UpdateRentalCommand(id,command.StatusId,command.Review));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRental(int id)
        {
            await _mediator.Send(new DeleteRentalCommand(id));
            return NoContent();
        }
    }
}
