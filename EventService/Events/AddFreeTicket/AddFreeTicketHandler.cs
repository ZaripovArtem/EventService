using Features.Events.AddEvent;
using Features.Events.Data;
using MediatR;

namespace Features.Events.AddFreeTicket
{
    public class AddFreeTicketHandler : IRequestHandler<AddFreeTicketCommand>
    {
        private readonly FakeData _fakeData;
        public AddFreeTicketHandler(FakeData fakeData)
        {
            _fakeData = fakeData;
        }

        public async Task Handle(AddFreeTicketCommand request, CancellationToken cancellationToken)
        {
            await _fakeData.AddFreeTicket(request.EventId, request.Count);
        }
    }
}
