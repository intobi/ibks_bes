using IBKS.Core.Interfaces;
using IBKS.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IBKS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous] // as authorization is not present, allowed anonymous for test
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;
    public TicketsController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<TicketListModel>), 200)]
    public async Task<IActionResult> GetTicketsAsync(CancellationToken cancellationToken = default)
    {
        var response = await _ticketService.GetAllAsync(cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TicketViewModel), 200)]
    public async Task<IActionResult> GetTicketAsync(long id, CancellationToken cancellationToken = default)
    {
        var response = await _ticketService.GetOneAsync(id, cancellationToken);
        return Ok(response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(void), 204)]
    public async Task<IActionResult> PutTicketAsync(long id, [FromBody] TicketViewModel request, CancellationToken cancellationToken = default)
    {
        await _ticketService.PutAsync(id, request, cancellationToken);
        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(typeof(void), 201)]
    public async Task<IActionResult> PostTicketAsync([FromBody]TicketPostModel request, CancellationToken cancellationToken = default)
    {
        await _ticketService.PostAsync(request, cancellationToken);
        return Created();
    }
}
