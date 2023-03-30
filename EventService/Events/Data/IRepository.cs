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
    // ReSharper disable once UnusedMemberInSuper.Global Событие интерфейса
    Task<Event?> GetEventById(Guid id);
    /// <summary>
    /// Получение списка мероприятий
    /// </summary>
    // ReSharper disable once UnusedMember.Global Список элементов
    Task<IEnumerable<Event>> GetAllEvents();
    /// <summary>
    /// Добавление мероприятия
    /// </summary>
    // ReSharper disable once UnusedMemberInSuper.Global Событие интерфейса
    Task AddEvent(Event events);
    /// <summary>
    /// Изменение информации мероприятия
    /// </summary>
    // ReSharper disable once UnusedMemberInSuper.Global Событие интерфейса
    Task UpdateEvent(Event events);
    /// <summary>
    /// Удаление мероприятия по его Id
    /// </summary>
    // ReSharper disable once UnusedMemberInSuper.Global Событие интерфейса
    Task DeleteEvent(Guid id);
    /// <summary>
    /// Добавить бесплатные билеты
    /// </summary>
    /// <param name="eventId">Id мероприятия, которому добавляются билеты</param>
    /// <param name="count">Количество билетов</param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global Событие интерфейса
    Task AddFreeTicket(Guid eventId, int count);
    /// <summary>
    /// Назначение билета пользователю
    /// </summary>
    /// <param name="eventId">Id мероприятия</param>
    /// <param name="ticketId">Id билета</param>
    /// <param name="userId">Id пользователя</param>
    /// <returns></returns>
    // ReSharper disable once UnusedMember.Global Событие интерфейса
    Task GiveTicketToUser(Guid eventId, Guid ticketId, Guid userId);
}
