using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentService.Application.Commands;
using RentService.Application.Queries;
using RentService.Domain.Entities;
using RentService.Application.Commands;


namespace RentService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RentalsController(IMediator mediator)
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

        [HttpGet("IsBookAvailable")]
        public async Task<ActionResult<bool>> IsBookAvailable([FromQuery] int bookId)
        {
            var result = await _mediator.Send(new IsBookAvailableQuery(bookId));
            return Ok(result);
        }

    }
}
