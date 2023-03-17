using MediatR;

namespace Features.Events.GiveTicketToUser;

public record GiveTicketToUserCommand(Guid EventId, Guid TicketId, Guid UserId) : IRequest;
