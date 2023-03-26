using Features.Events.Domain;
using Features.Settings;
using MongoDB.Driver;

namespace Features.Events.Data;

/// <summary>
/// Класс для работы с MongoDb
/// </summary>
public class Repository : IRepository
{
    private readonly IMongoCollection<Event> _events;
    private readonly IMessageService _messageService;
    private readonly IMessageService _imageMessageService;
    private readonly IMessageService _spaceMessageService;
    /// <summary>
    /// Список Id фотографий
    /// </summary>
    public List<Guid> Image;
    /// <summary>
    /// Список Id комнат
    /// </summary>
    public List<Guid> Room;
    /// <summary>
    /// Список пользователей
    /// </summary>
    public List<User> User;

    /// <summary>
    /// Конструктор. В нем происходит обращение к MongoDb, а также добавление начальных
    /// значений для Image и Room
    /// </summary>
    public Repository()
    {
        var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
        var dbName = Environment.GetEnvironmentVariable("DB_NAME");
        var connectionString = $"mongodb://{dbHost}:27017/{dbName}";

        var mongodbUrl = MongoUrl.Create(connectionString);
        var mongodbClient = new MongoClient(mongodbUrl);
        var database = mongodbClient.GetDatabase(mongodbUrl.DatabaseName);
        _events = database.GetCollection<Event>("order");

        Image = new List<Guid>(GetImageList().Result.ToList());

        Room = new List<Guid>(GetRoomList().Result.ToList());

        User = new List<User>
        {
            new()
            {
                Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa2"),
                NickName = "Nick"
            }
        };

        IMessageService messageService = new MessageService();
        _messageService = messageService;
        IMessageService imageMessageService = new ImageMessageService();
        _imageMessageService = imageMessageService;
        IMessageService spaceMessageService = new SpaceMessageService();
        _spaceMessageService = spaceMessageService;
    }

    /// <summary>
    /// Получение мероприятия по его Id
    /// </summary>
    /// <param name="id">Id мероприятия</param>
    /// <returns></returns>
    public async Task<Event?> GetEventById(Guid id)
    {
        return await _events.Find(Builders<Event>.Filter.Eq(e => e.Id, id)).FirstOrDefaultAsync();
    }

    /// <summary>
    /// Получение списка всех мероприятий
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Event>> GetAllEvents()
    {
        return await _events.Find(Builders<Event>.Filter.Empty).ToListAsync();
    }

    /// <summary>
    /// Добавление мероприятия
    /// </summary>
    public async Task AddEvent(Event events)
    {
        await _events.InsertOneAsync(events);
        await Task.CompletedTask;
    }

    /// <summary>
    /// Обновление мероприятия
    /// </summary>
    public async Task UpdateEvent(Event events)
    {
        var filter = Builders<Event>.Filter.Eq(e => e.Id, events.Id);
        await _events.ReplaceOneAsync(filter, events);
        await Task.CompletedTask;
    }

    /// <summary>
    /// Удаление мероприятия
    /// </summary>
    public async Task DeleteEvent(Guid id)
    {
        var filter = Builders<Event>.Filter.Eq(e => e.Id, id);
        _messageService.Enqueue($"Удаление события {id}");
        await _events.DeleteOneAsync(filter);
        await Task.CompletedTask;
    }

    /// <summary>
    /// Добавление бесплатных билетов к мероприятию
    /// </summary>
    /// <param name="eventId">Id мероприятия</param>
    /// <param name="count">Количество добавляемых элементов</param>
    /// <returns></returns>
    public async Task AddFreeTicket(Guid eventId, int count)
    {
        var searchEvent = _events.Find(e => e.Id == eventId).FirstOrDefaultAsync();

        var filter = Builders<Event>.Filter.Eq(e => e.Id, eventId);
        Event newEvent = new()
        {
            Id = searchEvent.Result.Id,
            StartEvent = searchEvent.Result.StartEvent,
            EndEvent = searchEvent.Result.EndEvent,
            EventName = searchEvent.Result.EventName,
            PhotoId = searchEvent.Result.PhotoId,
            PlaceId = searchEvent.Result.PlaceId,
            Ticket = searchEvent.Result.Ticket,
            HasPlace = searchEvent.Result.HasPlace,
            Price = searchEvent.Result.Price
        };

        if (newEvent.HasPlace == false)
        {
            // ReSharper disable once SuggestVarOrType_BuiltInTypes лучше читаемость
            for (int i = 0; i < count; i++)
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

            for (int i = 0; i < count; i++)
            {
                // ReSharper disable once ArrangeObjectCreationWhenTypeNotEvident лучше читаемость
                newEvent?.Ticket?.Add(new()
                {
                    Id = Guid.NewGuid(),
                    Place = newEvent.Ticket.Count + 1
                });
            }
        }
        
        if (newEvent != null) await _events.ReplaceOneAsync(filter, newEvent);
        await Task.CompletedTask;
    }

    /// <summary>
    /// Запись билета на определенного пользователя
    /// </summary>
    /// <param name="eventId">Id мероприятия</param>
    /// <param name="ticketId">Id билета</param>
    /// <param name="userId">Id пользователя</param>
    /// <returns></returns>
    public async Task GiveTicketToUser(Guid eventId, Guid ticketId, Guid userId)
    {
        var searchEvent = _events.Find(e => e.Id == eventId).FirstOrDefaultAsync();

        var filter = Builders<Event>.Filter.Eq(e => e.Id, eventId);
        Event newEvent = new()
        {
            Id = searchEvent.Result.Id,
            StartEvent = searchEvent.Result.StartEvent,
            EndEvent = searchEvent.Result.EndEvent,
            EventName = searchEvent.Result.EventName,
            PhotoId = searchEvent.Result.PhotoId,
            PlaceId = searchEvent.Result.PlaceId,
            Ticket = searchEvent.Result.Ticket,
            HasPlace = searchEvent.Result.HasPlace,
            Price = searchEvent.Result.Price
        };

        if (newEvent.Price > 0)
        {
            const string createUrl = "http://host.docker.internal:7004/payment/create";
            const string confirmUrl = "http://host.docker.internal:7004/payment/confirm";
            const string cancelUrl = "http://host.docker.internal:7004/payment/cancel";

            HttpClientHandler handler = new();
            handler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            var httpClient = new HttpClient(handler);

            PaymentOperation? paymentOperation = await httpClient.GetFromJsonAsync<PaymentOperation>(createUrl);
            
            Guid? payId = paymentOperation?.Id;
            
            newEvent.Ticket?.FindAll(t => t.Id == ticketId)
                .ForEach(u => u.UserId = userId);
            await _events.ReplaceOneAsync(filter, newEvent);

            searchEvent = _events.Find(e => e.Id == eventId).FirstOrDefaultAsync();
            var searchTicket = searchEvent.Result.Ticket?.Find(t => t.UserId == userId);

            if (searchTicket != null)
            {
                using var response = await httpClient.PutAsJsonAsync($"{confirmUrl}/{payId}", payId);
            }
            else
            {
                using var response = await httpClient.PutAsJsonAsync($"{cancelUrl}/{payId}", payId);
            }

        }
        else
        {
            newEvent.Ticket?.FindAll(t => t.Id == ticketId)
                .ForEach(u => u.UserId = userId);
            await _events.ReplaceOneAsync(filter, newEvent);
        }

        
    }

    /// <summary>
    /// Task для получения Id комнат
    /// </summary>
    /// <returns></returns>
    public async Task<List<Guid>> GetRoomList()
    {
        const string url = "http://host.docker.internal:7002/room";

        HttpClientHandler handler = new();
        handler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        var httpClient = new HttpClient(handler);

        var list = await httpClient.GetFromJsonAsync<List<Guid>>(url);

        return list!;
    }

    /// <summary>
    /// Task для получения Id изображения
    /// </summary>
    /// <returns></returns>
    public async Task<List<Guid>> GetImageList()
    {
        const string url = "http://host.docker.internal:7003/image";

        HttpClientHandler handler = new();
        handler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        var httpClient = new HttpClient(handler);

        var list = await httpClient.GetFromJsonAsync<List<Guid>>(url);

        return list!;
    }

    private class PaymentOperation
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local Используется для httpClient
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Удаление пространства
    /// </summary>
    /// <param name="id">Id пространства</param>
    /// <returns></returns>
    public Task DeleteSpace(Guid id)
    {
        _spaceMessageService.Enqueue($"Удаление пространства {id}");
        return Task.CompletedTask;
    }

    /// <summary>
    /// Удаление афиши
    /// </summary>
    /// <param name="id">Id афиши</param>
    /// <returns></returns>
    public Task DeleteImage(Guid id)
    {
        _imageMessageService.Enqueue($"Удаление афиши {id}");
        return Task.CompletedTask;
    }
}
