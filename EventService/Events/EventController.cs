using EventService.Events.GetEvent;
using Features.Events.AddEvent;
using Features.Events.AddFreeTicket;
using Features.Events.DeleteEvent;
using Features.Events.Domain;
using Features.Events.GetEvent;
using Features.Events.GiveTicketToUser;
using Features.Events.UpdateEvent;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Features.Events
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IMediator _mediator;

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
        [HttpPut("{id}")]
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
        [HttpDelete("{id}")]
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
        /// Добавление бесплатных билетов для определенного мероприятия
        /// </summary>
        /// <param name="id">Id мероприятия</param>
        /// <param name="count">Количество билетов для добавления</param>
        /// <returns></returns>
        [HttpPost("/AddFreeTicket")]
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
        [HttpPost("/GiveTicket")]
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
    }
}
