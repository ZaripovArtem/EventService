using Features.Events.Data;
using Features.Events.Domain;
using MediatR;
using MongoDB.Driver;

namespace Features.Events.DeleteEvent
{
    /// <summary>
    /// Реализация обработчика DeleteEventCommand
    /// </summary>
    // ReSharper disable once UnusedMember.Global Обработчик
    public class DeleteEventHandler : IRequestHandler<DeleteEventCommand>
    {
        private readonly Repository _data;

        /// <summary>
        /// Конструктор, для получения данных с MongoDb
        /// </summary>
        public DeleteEventHandler(Repository data)
        {
            _data = data;
        }

        /// <summary>
        /// Удаление мероприятия
        /// </summary>
        public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            //await _data.DeleteEvent(request.Id);
            var filter = Builders<Event>.Filter.Eq(e => e.Id, request.Id);
            _data.MessageService.Enqueue($"Удаление события {request.Id}");
            await _data.Events.DeleteOneAsync(filter, cancellationToken);
        }
    }
}
