using Features.Events.Data;
using Features.Events.Domain;
using MediatR;

namespace Features.Events.GetEvent
{
    /// <summary>
    /// Реализация обработчика GetEventByIdQuery
    /// </summary>
    public class GetEventByIdHandler : IRequestHandler<GetEventByIdQuery, Event>
    {
        private readonly Repository _data;

        /// <summary>
        /// Конструктор, для получения данных с MongoDb
        /// </summary>
        public GetEventByIdHandler(Repository data)
        {
            _data = data;
        }

        /// <summary>
        /// Реализация обработчика
        /// </summary>
        public async Task<Event> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            return await _data.GetEventById(request.Id) ?? throw new InvalidOperationException();
        }
    }
}
