using Microsoft.AspNetCore.Mvc;
using GloboTicket.Catalog.Repositories;

namespace GloboTicket.Catalog.Controllers;

[ApiController]
[Route("[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventRepository _eventRepository;

    private static int callcounter = 0;
    private readonly ILogger<EventController> _logger;

    public EventController(IEventRepository eventRepository, ILogger<EventController> logger)
    {
        _eventRepository = eventRepository;
        _logger = logger;
    }

    [HttpGet(Name = "GetEvents")]
    public async Task<IActionResult> GetAll()
    {
        // let 1 out of 4 requests fail
        if (callcounter++ % 4 == 0)
        {
            Thread.Sleep(2000);
            HttpContext.Abort();
            return Ok();
        }
        else
            return  Ok(await _eventRepository.GetEvents());
    }

    [HttpGet("{id}", Name = "GetById")]
    public async Task<Event> GetById(Guid id)
    {
        return await _eventRepository.GetEventById(id);
    }
}
