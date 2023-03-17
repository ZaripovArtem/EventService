namespace Features.Events.Domain;

public class Event
{
    public Guid Id { get; set; }
    public DateTime StartEvent { get; set; }
    public DateTime EndEvent { get; set; }
    public string? EventName { get; set; }
    public Guid? PhotoId { get; set; }
    public Guid PlaceId { get; set; }
    public List<Ticket>? Ticket { get; set; }
}
