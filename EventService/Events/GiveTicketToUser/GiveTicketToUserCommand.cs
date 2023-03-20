using MediatR;

namespace Features.Events.GiveTicketToUser;

/// <summary>
/// Реализация интерфейса команды
/// </summary>
public record GiveTicketToUserCommand(Guid EventId, Guid TicketId, Guid UserId) : IRequest;