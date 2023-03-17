using Features.Events.Domain;

namespace Features.Events.Data;

public class FakeData
{
    private List<Event> _events;
    public List<Guid> Image;
    public List<Guid> Room;
    public List<Ticket> Ticket;
    public List<User> User;
    public FakeData()
    {
        // Добавление тестовых данных
        _events = new List<Event> 
        {
            new() { Id = Guid.NewGuid(), EventName = "qwe", StartEvent = DateTime.Now, EndEvent = DateTime.Now, 
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
                Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa1"), Place = null,
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

    public async Task<Event?> GetEventById(Guid id) 
    {
        return await Task.FromResult(_events.FirstOrDefault(e => e.Id == id));
    }

    public async Task<IEnumerable<Event>> GetAllEvents()
    {
        return await Task.FromResult(_events);
    }

    public async Task AddEvent(Event events)
    {
        _events.Add(events);
        await Task.CompletedTask;
    }

    public async Task UpdateEvent(Event events)
    {
        int index = _events.FindIndex(e => e.Id == events.Id);
        _events.RemoveAt(index);
        _events.Insert(index, events);
        await Task.CompletedTask;
    }

    public async Task DeleteEvent(Guid id)
    {
        int index = _events.FindIndex(e => e.Id == id);
        _events.RemoveAt(index);
        await Task.CompletedTask;
    }

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

    public async Task GiveTicketToUser(Guid eventId, Guid ticketId, Guid userId)
    {
        var searchEvent = _events.FindAll(e => e.Id == eventId).FirstOrDefault();

        searchEvent?.Ticket?.FindAll(t => t.Id == ticketId)
            .ForEach(u => u.UserId = userId);

        await Task.CompletedTask;
    }
}
