namespace Features.Settings;

/// <summary>
/// Интерфейс продюссера
/// </summary>
public interface IMessageService
{
    /// <summary>
    /// Наличие сообщения
    /// </summary>
    /// <param name="message">Сообщение</param>
    /// <returns></returns>
    bool Enqueue(string message);
}

