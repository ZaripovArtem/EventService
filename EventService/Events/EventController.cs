using Features.Events.AddEvent;
using Features.Events.AddFreeTicket;
using Features.Events.DeleteEvent;
using Features.Events.DeleteImage;
using Features.Events.DeleteSpace;
using Features.Events.Domain;
using Features.Events.GetEvent;
using Features.Events.GiveTicketToUser;
using Features.Events.UpdateEvent;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Features.Events
{
    /// <summary>
    /// Контроллер Event
    /// </summary>
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IMediator _mediator;
        /// <summary>
        /// Конструктор EventController
        /// </summary>
        /// <param name="mediator">DI mediator</param>
        public EventController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        /// <summary>
        /// Получение списка всех мероприятий
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var result = await _mediator.Send(new GetAllEventsQuery());
            return Ok(result);
        }

        /// <summary>
        /// Добавление мероприятия
        /// </summary>
        /// <param name="events">Параметр запроса</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddEvent([FromBody] Event events)
        {
            await _mediator.Send(new AddEventCommand(events));
            return StatusCode(201);
        }

        /// <summary>
        /// Изменение мероприятия по его Id
        /// </summary>
        /// <param name="id">Id мероприятия</param>
        /// <param name="events">Параметр запроса</param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] Event events)
        {
            if(id != events.Id)
            {
                return BadRequest("Свойство Id менять запрещено");
            }

            await _mediator.Send(new GetEventByIdQuery(events.Id));

            await _mediator.Send(new UpdateEventCommand(events));
            return StatusCode(200);
        }

        /// <summary>
        /// Удаление мероприятия по его Id
        /// </summary>
        /// <param name="id">Id мероприятия</param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            try
            {
                await _mediator.Send(new GetEventByIdQuery(id));
            }
            catch
            {
                return BadRequest("Событие не найдено");
            }

            await _mediator.Send(new DeleteEventCommand(id));
            return StatusCode(200);
        }

        /// <summary>
        /// Добавление билетов для определенного мероприятия
        /// </summary>
        /// <param name="id">Id мероприятия</param>
        /// <param name="count">Количество билетов для добавления</param>
        /// <returns></returns>
        [HttpPost("{id:guid}/Ticket")]
        public async Task<IActionResult> AddFreeTickets(Guid id, int count)
        {
            try
            {
                await _mediator.Send(new GetEventByIdQuery(id));
            }
            catch
            {
                return BadRequest("Событие не найдено");
            }

            await _mediator.Send(new AddFreeTicketCommand(id, count));
            return StatusCode(200);
        }

        /// <summary>
        /// Присвоение билета User'у
        /// </summary>
        /// <param name="eventId">Id мероприятия</param>
        /// <param name="userId">Id пользователя</param>
        /// <param name="ticketId">Id билета</param>
        /// <returns></returns>
        [HttpPut("{eventId}/Ticket/{ticketId:guid}/User")]
        public async Task<IActionResult> GiveTicketToUser(Guid eventId, Guid userId, Guid ticketId)
        {
            try
            {
                await _mediator.Send(new GetEventByIdQuery(eventId));
            }
            catch
            {
                return BadRequest("Событие не найдено");
            }

            await _mediator.Send(new GiveTicketToUserCommand(eventId, ticketId, userId));
            return StatusCode(200);
        }

        /// <summary>
        /// Проверка билета на заданное мероприятие у пользователя
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="eventId">Id мероприятия</param>
        /// <returns></returns>
        [HttpGet("{eventId:guid}/User/{userId:guid}")]
        public async Task<IActionResult> CheckTicket(Guid userId, Guid eventId)
        {
            var events = await _mediator.Send(new GetEventByIdQuery(eventId));
            var result = "Не найдено";

            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract Проверка на мероприятие
            if (events == null) return Ok(result);
            foreach (var ticket in events.Ticket!.Where(ticket => ticket.UserId == userId))
            {
                result = ticket.Place.ToString();
            }

            return Ok(result);
        }

        /// <summary>
        /// Удаление афиши
        /// </summary>
        /// <param name="id">Id афиши</param>
        /// <returns></returns>
        [HttpDelete("/Image/{id:guid}")]
        public async Task<IActionResult> DeleteImage(Guid id)
        {
            await _mediator.Send(new DeleteImageCommand(id));
            return StatusCode(200);
        }

        /// <summary>
        /// Удаление пространства
        /// </summary>
        /// <param name="id">Id пространства</param>
        /// <returns></returns>
        [HttpDelete("/Space/{id:guid}")]
        public async Task<IActionResult> DeleteSpace(Guid id)
        {
            await _mediator.Send(new DeleteSpaceCommand(id));
            return StatusCode(200);
        }
    }
}
