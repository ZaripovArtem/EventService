using EventService.Events.GetEvent;
using Features.Events.Data;
using Features.Events.Domain;
using MediatR;

namespace Features.Events.GetEvent
{
    public class GetAllEventsHandler : IRequestHandler<GetAllEventsQuery, IEnumerable<Event>>
    {
        private readonly FakeData _fakeData;
        public GetAllEventsHandler(FakeData fakeData)
        {
            _fakeData = fakeData;
        }

        public async Task<IEnumerable<Event>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            return await _fakeData.GetAllEvents();
        }
    }
}
