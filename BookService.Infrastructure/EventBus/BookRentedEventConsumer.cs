using BookService.Application.Commands;
using MassTransit;
using MediatR;
using SharedContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookService.Infrastructure.EventBus
{
    public sealed class BookRentedEventConsumer : IConsumer<BookRentedEvent>
    {
        private readonly IMediator _mediator;

        public BookRentedEventConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<BookRentedEvent> context)
        {
            await _mediator.Send(
                new MarkBookAsRentedCommand(context.Message.BookId),
                context.CancellationToken
            );
        }
    }
}
