using Features.Events.Data;
using MediatR;

namespace Features.Events.AddFreeTicket
{
    /// <summary>
    /// Реализация обработчика AddFreeTicketCommand
    /// </summary>
    public class AddFreeTicketHandler : IRequestHandler<AddFreeTicketCommand>
    {
        private readonly Repository _data;

        /// <summary>
        /// Конструктор, для получения данных с MongoDb
        /// </summary>
        public AddFreeTicketHandler(Repository data)
        {
            _data = data;
        }

        /// <summary>
        /// Реализация обработчика
        /// </summary>
        public async Task Handle(AddFreeTicketCommand request, CancellationToken cancellationToken)
        {
            await _data.AddFreeTicket(request.EventId, request.Count);
        }
    }
}
