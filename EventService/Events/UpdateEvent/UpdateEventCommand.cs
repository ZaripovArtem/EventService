using Features.Events.Domain;
using MediatR;

namespace Features.Events.UpdateEvent;

/// <summary>
/// Реализация интерфейса команды
/// </summary>
public record UpdateEventCommand(Event Event) : IRequest;