using EventService.Data;
using MediatR;

namespace EventService.Events.UpdateEvent
{
    public class UpdateEventHandler : IRequestHandler<UpdateEventCommand>
    {
        private readonly FakeData _fakeData;
        public UpdateEventHandler(FakeData fakeData)
        {
            _fakeData = fakeData;
        }
        public async Task Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            await _fakeData.UpdateEvent(request.Event);
        }
    }
}
