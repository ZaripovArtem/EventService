using Features.Events.Domain;
using MediatR;

namespace Features.Events.GetEvent;

/// <summary>
/// Реализация интерфейса команды
/// </summary>
public record GetEventByIdQuery(Guid Id) : IRequest<Event>;
