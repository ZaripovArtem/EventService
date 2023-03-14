using EventService.Data;
using MediatR;

namespace EventService.Events.GetEvent
{
    public class GetEventByIdHandler : IRequestHandler<GetEventByIdQuery, Event>
    {
        private readonly FakeData _fakeData;
        public GetEventByIdHandler(FakeData fakeData)
        {
            _fakeData = fakeData;
        }

        public async Task<Event> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            return await _fakeData.GetEventById(request.id);
        }
    }
}
