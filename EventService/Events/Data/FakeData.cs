namespace EventService.Data;

public class FakeData
{
    private List<Event> _events;
    public List<Guid> _image;
    public List<Guid> _room;
    public FakeData()
    {
        // Добавление тестовых данных
        _events = new List<Event> 
        {
            new Event { Id = Guid.NewGuid(), EventName = "qwe", StartEvent = DateTime.Now, EndEvent = DateTime.Now }
        };

        _image = new List<Guid>
        {
            new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6")
        };
        
        _room = new List<Guid>
        {
            new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6")
        };
    }

    public async Task<Event> GetEventById(Guid id) 
    {
        return await Task.FromResult(_events.Where(e => e.Id == id).FirstOrDefault());
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
}
