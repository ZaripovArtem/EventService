using Features.Events.Data;
using MediatR;

namespace Features.Events.DeleteEvent
{
    public class DeleteEventHandler : IRequestHandler<DeleteEventCommand>
    {
        private readonly FakeData _fakeData;
        public DeleteEventHandler(FakeData fakeData)
        {
            _fakeData = fakeData;
        }

        public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            await _fakeData.DeleteEvent(request.Id);
        }
    }
}
