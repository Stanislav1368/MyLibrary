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
    public class BookReturnedEventConsumer : IConsumer<BookReturnedEvent>
    {
        private readonly IMediator _mediator;

        public BookReturnedEventConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<BookReturnedEvent> context)
        {
            await _mediator.Send(
                new MarkBookAsAvailableCommand(context.Message.BookId),
                context.CancellationToken
            );
        }
    }
}
