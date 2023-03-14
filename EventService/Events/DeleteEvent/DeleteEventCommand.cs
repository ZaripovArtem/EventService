using MediatR;

namespace EventService.Commands
{
    public record DeleteEventCommand(Guid id) : IRequest;
}
