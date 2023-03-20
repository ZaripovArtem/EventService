namespace Features.Events.Domain;

/// <summary>
/// Класс пользователей
/// </summary>
public class User
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Nick-Name пользователя
    /// </summary>
    public string? NickName { get; set; }
}