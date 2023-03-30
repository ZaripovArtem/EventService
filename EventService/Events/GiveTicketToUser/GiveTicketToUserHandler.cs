using AutoMapper;
using Features.Events.Data;
using Features.Events.Domain;
using MediatR;
using MongoDB.Driver;
using Payment;

namespace Features.Events.GiveTicketToUser;

/// <summary>
/// Реализация обработчика GiveTicketToUserCommand
/// </summary>
// ReSharper disable once UnusedMember.Global Обработчик
public class GiveTicketToUserHandler : IRequestHandler<GiveTicketToUserCommand>
{
    private readonly Repository _data;
    private readonly IMapper _mapper;

    /// <summary>
    /// Конструктор, для получения данных с MongoDb
    /// </summary>
    public GiveTicketToUserHandler(Repository data, IMapper mapper)
    {
        _data = data;
        _mapper = mapper;
    }

    /// <summary>
    /// Запись билета на определенного пользователя
    /// </summary>
    public async Task Handle(GiveTicketToUserCommand request, CancellationToken cancellationToken)
    {
        //await _data.GiveTicketToUser(request.EventId, request.TicketId, request.UserId);
        var searchEvent = _data.Events.Find(e => e.Id == request.EventId).FirstOrDefault();

        var filter = Builders<Event>.Filter.Eq(e => e.Id, request.EventId);

        Event newEvent = _mapper.Map<Event>(searchEvent);

        if (newEvent.Price > 0)
        {
            const string createUrl = "http://host.docker.internal:7004/payment/create";
            const string confirmUrl = "http://host.docker.internal:7004/payment/confirm";
            const string cancelUrl = "http://host.docker.internal:7004/payment/cancel";

            HttpClientHandler handler = new();
            handler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var httpClient = new HttpClient(handler);

            //PaymentOperation? paymentOperation = await httpClient.GetFromJsonAsync<PaymentOperation>(createUrl, cancellationToken: cancellationToken);

            PaymentOperation paymentOperation = new PaymentOperation
            {
                Id = Guid.NewGuid(),
                State = PaymentState.Hold,
                DateCreation = DateTimeOffset.Now
            };

            var payId = paymentOperation.Id;

            await httpClient.PostAsJsonAsync(createUrl, paymentOperation, cancellationToken: cancellationToken);

            newEvent.Ticket?.FindAll(t => t.Id == request.TicketId)
                .ForEach(u => u.UserId = request.UserId);
            await _data.Events.ReplaceOneAsync(filter, newEvent, cancellationToken: cancellationToken);

            searchEvent = _data.Events.Find(e => e.Id == request.EventId)
                .FirstOrDefault();
            var searchTicket = searchEvent.Ticket?.Find(t => t.UserId == request.UserId);

            if (searchTicket != null)
            {
                using var response = await httpClient.PutAsJsonAsync($"{confirmUrl}/{payId}", payId,
                    cancellationToken: cancellationToken);
            }
            else
            {
                using var response = await httpClient.PutAsJsonAsync($"{cancelUrl}/{payId}", payId,
                    cancellationToken: cancellationToken);
            }
        }
        else
        {
            newEvent.Ticket?.FindAll(t => t.Id == request.TicketId)
                .ForEach(u => u.UserId = request.UserId);
            await _data.Events.ReplaceOneAsync(filter, newEvent, cancellationToken: cancellationToken);
        }
    }
}
