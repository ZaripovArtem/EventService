using EventService.Data;
using MediatR;

namespace EventService.Events.GetEvent;

public record GetEventByIdQuery(Guid id) : IRequest<Event>;
