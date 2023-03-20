using Features.Events.Data;
using Features.Events.Domain;
using MediatR;

namespace Features.Events.GetEvent
{
    /// <summary>
    /// Реализация обработчика GetAllEventsQuery
    /// </summary>
    public class GetAllEventsHandler : IRequestHandler<GetAllEventsQuery, IEnumerable<Event>>
    {
        private readonly Repository _data;

        /// <summary>
        /// Конструктор, для получения данных с MongoDb
        /// </summary>
        public GetAllEventsHandler(Repository data)
        {
            _data = data;
        }

        /// <summary>
        /// Реализация обработчика
        /// </summary>
        public async Task<IEnumerable<Event>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            return await _data.GetAllEvents();
        }
    }
}
