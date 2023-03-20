using Features.Events.Domain;

namespace Features.Events.Data;

/// <summary>
/// Класс-репозиторий для тестовых (локальных) значений
/// </summary>
public class FakeData : IRepository
{
    private List<Event> _events;
    /// <summary>
    /// Список Id изображений
    /// </summary>
    public List<Guid> Image;
    /// <summary>
    /// Список Id комнат
    /// </summary>
    public List<Guid> Room;
    /// <summary>
    /// Список билетов
    /// </summary>
    public List<Ticket> Ticket;
    /// <summary>
    /// Список пользователей
    /// </summary>
    public List<User> User;

    /// <summary>
    /// Конструктор класса, в котором происходит добавление начальных значений
    /// </summary>
    public FakeData()
    {
        _events = new List<Event> 
        {
            new() { Id = Guid.NewGuid(), EventName = "Event1", StartEvent = DateTime.Now, EndEvent = DateTime.Now, 
                Ticket = new List<Ticket>()
                {
                    new()
                    {
                        Id = Guid.NewGuid()
                    }
                }}
        };

        Image = new List<Guid>
        {
            new("3fa85f64-5717-4562-b3fc-2c963f66afa6")
        };
        
        Room = new List<Guid>
        {
            new("3fa85f64-5717-4562-b3fc-2c963f66afa6")
        };

        Ticket = new List<Ticket>
        {
            new()
            {
                Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa1"), Place = 0,
                UserId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa2")
            }
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
    /// Получение информации о мероприятии по его Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Event?> GetEventById(Guid id) 
    {
        return await Task.FromResult(_events.FirstOrDefault(e => e.Id == id));
    }

    /// <summary>
    /// Получение списка всех мероприятий
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Event>> GetAllEvents()
    {
        return await Task.FromResult(_events);
    }

    /// <summary>
    /// Добавление мероприятия
    /// </summary>
    public async Task AddEvent(Event events)
    {
        _events.Add(events);
        await Task.CompletedTask;
    }

    /// <summary>
    /// Обновление мероприятия
    /// </summary>
    public async Task UpdateEvent(Event events)
    {
        int index = _events.FindIndex(e => e.Id == events.Id);
        _events.RemoveAt(index);
        _events.Insert(index, events);
        await Task.CompletedTask;
    }

    /// <summary>
    /// Удаление мероприятия
    /// </summary>
    public async Task DeleteEvent(Guid id)
    {
        int index = _events.FindIndex(e => e.Id == id);
        _events.RemoveAt(index);
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
        var searchEvent = _events.FindAll(e => e.Id == eventId).FirstOrDefault();

        for (int i = 0; i < count; i++)
        {
            searchEvent?.Ticket?.Add(new()
            {
                Id = Guid.NewGuid()
            });
        }

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
        var searchEvent = _events.FindAll(e => e.Id == eventId).FirstOrDefault();

        searchEvent?.Ticket?.FindAll(t => t.Id == ticketId)
            .ForEach(u => u.UserId = userId);

        await Task.CompletedTask;
    }
}
