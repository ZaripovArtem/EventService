using Features.Events.Data;
using MediatR;

namespace Features.Events.UpdateEvent
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
