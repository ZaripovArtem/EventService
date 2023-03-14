using EventService.Data;
using MediatR;

namespace EventService.Events.AddEvent;

public record AddEventCommand(Event Event) : IRequest;
