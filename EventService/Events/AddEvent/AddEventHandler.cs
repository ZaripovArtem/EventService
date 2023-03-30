using Features.Events.Data;
using MediatR;

namespace Features.Events.AddEvent
{
    /// <summary>
    /// Реализация обработчика AddEventCommand
    /// </summary>
    // ReSharper disable once UnusedMember.Global Обработчик
    public class AddEventHandler : IRequestHandler<AddEventCommand>
    {
        private readonly Repository _data;

        /// <summary>
        /// Конструктор, для получения данных с MongoDb
        /// </summary>
        public AddEventHandler(Repository data)
        {
            _data = data;
        }

        /// <summary>
        /// Получение списка всех мероприятий
        /// </summary>
        public async Task Handle(AddEventCommand request, CancellationToken cancellationToken)
        {
            await _data.Events.InsertOneAsync(request.Event, cancellationToken: cancellationToken);
        }
    }
}
