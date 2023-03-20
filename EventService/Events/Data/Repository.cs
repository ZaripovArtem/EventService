using Features.Events.Domain;
using MongoDB.Driver;

namespace Features.Events.Data;

/// <summary>
/// Класс для работы с MongoDb
/// </summary>
public class Repository : IRepository
{
    private readonly IMongoCollection<Event> _events;
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

        var mongoUrl = MongoUrl.Create(connectionString);
        var mongoClient = new MongoClient(mongoUrl);
        var database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        _events = database.GetCollection<Event>("order");

        Image = new List<Guid>
        {
            new("3fa85f64-5717-4562-b3fc-2c963f66afa6")
        };

        Room = new List<Guid>
        {
            new("3fa85f64-5717-4562-b3fc-2c963f66afa6")
        };

        User = new List<User>
        {
            new()
            {
                Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa2"),
                NickName = "Nick"
            }
        };
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
            HasPlace = searchEvent.Result.HasPlace
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
            HasPlace = searchEvent.Result.HasPlace
        };

        newEvent.Ticket?.FindAll(t => t.Id == ticketId)
            .ForEach(u => u.UserId = userId);

        await _events.ReplaceOneAsync(filter, newEvent);
    }

}
