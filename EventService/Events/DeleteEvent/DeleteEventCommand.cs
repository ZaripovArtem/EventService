using MediatR;

namespace Features.Events.DeleteEvent
{
    public record DeleteEventCommand(Guid Id) : IRequest;
}