using Features.Events.Data;
using MediatR;

namespace Features.Events.AddEvent
{
    public class AddEventHandler : IRequestHandler<AddEventCommand>
    {
        private readonly FakeData _fakeData;
        public AddEventHandler(FakeData fakeData)
        {
            _fakeData = fakeData;
        }

        public async Task Handle(AddEventCommand request, CancellationToken cancellationToken)
        {
            await _fakeData.AddEvent(request.Event);
        }
    }
}
