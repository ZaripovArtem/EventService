using Features.Events.Data;
using MediatR;

namespace Features.Events.DeleteEvent
{
    /// <summary>
    /// Реализация обработчика DeleteEventCommand
    /// </summary>
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
        /// Реализация обработчика
        /// </summary>
        public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            await _data.DeleteEvent(request.Id);
        }
    }
}
