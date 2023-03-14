using EventService.Data;
using MediatR;

namespace EventService.Events.GetEvent;

public record GetAllEventsQuery() : IRequest<IEnumerable<Event>>;
