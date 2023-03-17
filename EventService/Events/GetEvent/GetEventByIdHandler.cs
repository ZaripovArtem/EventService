using EventService.Events.GetEvent;
using Features.Events.Data;
using Features.Events.Domain;
using MediatR;

namespace Features.Events.GetEvent
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
            return await _fakeData.GetEventById(request.Id) ?? throw new InvalidOperationException();
        }
    }
}
