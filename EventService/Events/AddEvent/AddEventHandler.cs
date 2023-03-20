using Features.Events.Data;
using MediatR;

namespace Features.Events.AddEvent
{
    /// <summary>
    /// Реализация обработчика AddEventCommand
    /// </summary>
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
        /// Реализация обработчика
        /// </summary>
        public async Task Handle(AddEventCommand request, CancellationToken cancellationToken)
        {
            await _data.AddEvent(request.Event);
        }
    }
}
