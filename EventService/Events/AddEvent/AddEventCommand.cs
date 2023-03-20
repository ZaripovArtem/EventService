using Features.Events.Domain;
using MediatR;

namespace Features.Events.AddEvent;

/// <summary>
/// Реализация интерфейса команды
/// </summary>
public record AddEventCommand(Event Event) : IRequest;
