using Features.Events.Domain;
using MediatR;

namespace Features.Events.GetEvent;

/// <summary>
/// Реализация интерфейса запроса
/// </summary>
public record GetAllEventsQuery : IRequest<IEnumerable<Event>>, IRequest<Event>;