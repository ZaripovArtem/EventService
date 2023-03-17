using Features.Events.Domain;
using MediatR;

namespace EventService.Events.GetEvent;

public record GetEventByIdQuery(Guid Id) : IRequest<Event>;
