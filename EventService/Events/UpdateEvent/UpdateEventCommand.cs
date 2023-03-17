using Features.Events.Domain;
using MediatR;

namespace Features.Events.UpdateEvent;

public record UpdateEventCommand(Event Event) : IRequest;