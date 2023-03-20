namespace Features.Events.Domain;

/// <summary>
/// Класс билетов
/// </summary>
public class Ticket
{
    /// <summary>
    /// Id билета
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Id пользователя, которому принадлежит билет
    /// (при наличии)
    /// </summary>
    public Guid? UserId { get; set; }
    /// <summary>
    /// Id пространства
    /// </summary>
    public int Place { get; set; }
}