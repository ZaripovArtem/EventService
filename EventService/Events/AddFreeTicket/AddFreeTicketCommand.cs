using Features.Events.Domain;
using MediatR;

namespace Features.Events.AddFreeTicket;

public record AddFreeTicketCommand(Guid EventId, int Count) : IRequest;
