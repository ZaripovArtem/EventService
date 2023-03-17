using Features.Events.Domain;
using MediatR;

namespace Features.Events.GetEvent;

public record GetAllEventsQuery : IRequest<IEnumerable<Event>>;