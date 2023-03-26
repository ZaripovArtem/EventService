using MediatR;

namespace Features.Events.DeleteImage;

/// <summary>
/// Реализация интерфейса команды
/// </summary>
public record DeleteImageCommand(Guid Id) : IRequest;