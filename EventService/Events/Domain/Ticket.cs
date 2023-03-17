namespace Features.Events.Domain;
public class Ticket
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public string? Place { get; set; }
}