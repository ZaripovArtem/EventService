using Features.Events.Data;
using MediatR;

namespace Features.Events.GiveTicketToUser;
public class GiveTicketToUserHandler : IRequestHandler<GiveTicketToUserCommand>
{
    private readonly FakeData _fakeData;
    public GiveTicketToUserHandler(FakeData fakeData)
    {
        _fakeData = fakeData;
    }

    public async Task Handle(GiveTicketToUserCommand request, CancellationToken cancellationToken)
    {
        await _fakeData.GiveTicketToUser(request.EventId, request.TicketId, request.UserId);
    }
}
