using AutoMapper;
using Features.Events.Data;
using Features.Events.Domain;
using MediatR;
using MongoDB.Driver;

namespace Features.Events.AddFreeTicket
{
    /// <summary>
    /// Реализация обработчика AddFreeTicketCommand
    /// </summary>
    // ReSharper disable once UnusedMember.Global Обработчик
    public class AddFreeTicketHandler : IRequestHandler<AddFreeTicketCommand>
    {
        private readonly Repository _data;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор, для получения данных с MongoDb
        /// </summary>
        public AddFreeTicketHandler(Repository data, IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
        }

        /// <summary>
        /// Реализация обработчика
        /// </summary>
        public async Task Handle(AddFreeTicketCommand request, CancellationToken cancellationToken)
        {
            var searchEvent = _data.Events.Find(e => e.Id == request.EventId).FirstOrDefault();
            var filter = Builders<Event>.Filter.Eq(e => e.Id, request.EventId);

            Event newEvent = _mapper.Map<Event>(searchEvent);

            if (newEvent.HasPlace == false)
            {
                // ReSharper disable once SuggestVarOrType_BuiltInTypes лучше читаемость
                for (int i = 0; i < request.Count; i++)
                {
                    // ReSharper disable once ArrangeObjectCreationWhenTypeNotEvident лучше читаемость 
                    newEvent?.Ticket?.Add(new()
                    {
                        Id = Guid.NewGuid(),
                        Place = 0
                    });
                }
            }
            else
            {
                // ReSharper disable once ConvertIfStatementToNullCoalescingAssignment лучше читаемость
                if (newEvent.Ticket == null)
                {
                    newEvent.Ticket = new List<Ticket>();
                }

                for (int i = 0; i < request.Count; i++)
                {
                    // ReSharper disable once ArrangeObjectCreationWhenTypeNotEvident лучше читаемость
                    newEvent?.Ticket?.Add(new()
                    {
                        Id = Guid.NewGuid(),
                        Place = newEvent.Ticket.Count + 1
                    });
                }
            }

            if (newEvent != null) await _data.Events.ReplaceOneAsync(filter, newEvent, cancellationToken: cancellationToken);
        }
    }
}
