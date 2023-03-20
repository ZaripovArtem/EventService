using Features.Events.Data;
using MediatR;

namespace Features.Events.UpdateEvent
{
    /// <summary>
    /// Реализация обработчика UpdateEventCommand
    /// </summary>
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
        /// Реализация обработчика
        /// </summary>
        public async Task Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            await _data.UpdateEvent(request.Event);
        }
    }
}
