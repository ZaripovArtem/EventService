using MediatR;

namespace Features.Events.DeleteSpace;

/// <summary>
/// Реализация интерфейса команды
/// </summary>
public record DeleteSpaceCommand(Guid Id) : IRequest;