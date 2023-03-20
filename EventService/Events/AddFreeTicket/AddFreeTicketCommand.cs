using MediatR;

namespace Features.Events.AddFreeTicket;

/// <summary>
/// Реализация интерфейса команды
/// </summary>
public record AddFreeTicketCommand(Guid EventId, int Count) : IRequest;
