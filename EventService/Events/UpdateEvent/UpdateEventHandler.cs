using Features.Events.Data;
using Features.Events.Domain;
using MediatR;
using MongoDB.Driver;

namespace Features.Events.UpdateEvent
{
    /// <summary>
    /// Реализация обработчика UpdateEventCommand
    /// </summary>
    // ReSharper disable once UnusedMember.Global Обработчик
    public class UpdateEventHandler : IRequestHandler<UpdateEventCommand>
    {
        private readonly Repository _data;

        /// <summary>
        /// Конструктор, для получения данных с MongoDb
        /// </summary>
        public UpdateEventHandler(Repository data)
        {
            _data = data;
        }

        /// <summary>
        /// Обновление мероприятия
        /// </summary>
        public async Task Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<Event>.Filter.Eq(e => e.Id, request.Event.Id);
            await _data.Events.ReplaceOneAsync(filter, request.Event, cancellationToken: cancellationToken);
        }
    }
}
