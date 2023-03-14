using EventService.Commands;
using EventService.Data;
using EventService.Events.AddEvent;
using EventService.Events.GetEvent;
using EventService.Events.UpdateEvent;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventService.Events
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

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var result = await _mediator.Send(new GetAllEventsQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent([FromBody] Event events)
        {
            await _mediator.Send(new AddEventCommand(events));

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] Event events)
        {
            if(id != events.Id)
            {
                return BadRequest("Свойство Id менять запрещено");
            }

            var existEvent = await _mediator.Send(new GetEventByIdQuery(events.Id));

            if (existEvent == null)
            {
                return BadRequest("Событие не найдено");
            }
            else
            {
                await _mediator.Send(new UpdateEventCommand(events));
                return StatusCode(200);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var existEvent = await _mediator.Send(new GetEventByIdQuery(id));

            if (existEvent == null)
            {
                return BadRequest("Событие не найдено");
            }
            else
            {
                await _mediator.Send(new DeleteEventCommand(id));
                return StatusCode(200);
            }
        }
    }
}
