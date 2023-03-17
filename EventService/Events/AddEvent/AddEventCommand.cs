using Features.Events.Domain;
using MediatR;

namespace Features.Events.AddEvent;

public record AddEventCommand(Event Event) : IRequest;
