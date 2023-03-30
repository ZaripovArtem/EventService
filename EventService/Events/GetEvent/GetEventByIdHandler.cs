using Features.Events.Data;
using Features.Events.Domain;
using MediatR;
using MongoDB.Driver;

namespace Features.Events.GetEvent
{
    /// <summary>
    /// Реализация обработчика GetEventByIdQuery
    /// </summary>
    // ReSharper disable once UnusedMember.Global Обработчик
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
        /// Получение мероприятия по его Id
        /// </summary>
        public async Task<Event> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            return await _data.Events.Find(Builders<Event>.Filter.Eq(e => e.Id, request.Id)).FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }
    }
}
