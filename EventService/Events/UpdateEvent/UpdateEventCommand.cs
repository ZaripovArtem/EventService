using EventService.Data;
using MediatR;

namespace EventService.Events.UpdateEvent;

public record UpdateEventCommand(Event Event) : IRequest;