﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using BookService.Application.Commands;
using BookService.Application.Queries;
using BookService.Domain.Entities;

namespace BookService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RenterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RenterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Renter>> CreateRenter(CreateRenterCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<Renter>>> GetAllRenters()
        {
            var result = await _mediator.Send(new GetAllRentersQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Renter>> GetRenter(int id)
        {
            var result = await _mediator.Send(new GetRenterByIdQuery(id));
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRenter(int id, UpdateRenterCommand command)
        {
            await _mediator.Send(new UpdateRenterCommand(id, command.FullName, command.Email, command.Phone));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRenter(int id)
        {
            await _mediator.Send(new DeleteRenterCommand(id));
            return NoContent();
        }
    }
}