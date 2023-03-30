using Features.Events.Data;
using Features.Events.Domain;
using MediatR;
using MongoDB.Driver;

namespace Features.Events.GetEvent
{
    /// <summary>
    /// Реализация обработчика GetAllEventsQuery
    /// </summary>
    // ReSharper disable once UnusedMember.Global Обработчик
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
        /// Получение списка всех мероприятий
        /// </summary>
        public async Task<IEnumerable<Event>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            return await _data.Events.Find(Builders<Event>.Filter.Empty).ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
