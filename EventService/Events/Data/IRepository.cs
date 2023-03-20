using Features.Events.Domain;

namespace Features.Events.Data;

/// <summary>
/// Интерфейс для реализации функций репозитория
/// </summary>
public interface IRepository
{
    /// <summary>
    /// Получение мероприятия по Id
    /// </summary>
    Task<Event?> GetEventById(Guid id);
    /// <summary>
    /// Получение списка мероприятий
    /// </summary>
    Task<IEnumerable<Event>> GetAllEvents();
    /// <summary>
    /// Добавление мероприятия
    /// </summary>
    Task AddEvent(Event events);
    /// <summary>
    /// Изменение информации мероприятия
    /// </summary>
    Task UpdateEvent(Event events);
    /// <summary>
    /// Удаление мероприятия по его Id
    /// </summary>
    Task DeleteEvent(Guid id);
    /// <summary>
    /// Добавить бесплатные билеты
    /// </summary>
    /// <param name="eventId">Id мероприятия, которому добавляются билеты</param>
    /// <param name="count">Количество билетов</param>
    /// <returns></returns>
    Task AddFreeTicket(Guid eventId, int count);
    /// <summary>
    /// Назначение билета пользователю
    /// </summary>
    /// <param name="eventId">Id мероприятия</param>
    /// <param name="ticketId">Id билета</param>
    /// <param name="userId">Id пользователя</param>
    /// <returns></returns>
    Task GiveTicketToUser(Guid eventId, Guid ticketId, Guid userId);
}
