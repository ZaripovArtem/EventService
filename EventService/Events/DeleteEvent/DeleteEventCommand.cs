using MediatR;

namespace Features.Events.DeleteEvent;

/// <summary>
/// Реализация интерфейса команды
/// </summary>
public record DeleteEventCommand(Guid Id) : IRequest;