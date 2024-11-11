using IBKS.Core.Interfaces;
using IBKS.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IBKS.API.Controllers;
[Route("api/Tickets/{ticketId}/[controller]")]
[ApiController]
[AllowAnonymous] // as authorization is not present, allowed anonymous for test
public class ReplyController : ControllerBase
{
    private readonly IReplyService _replyService;
    public ReplyController(IReplyService replyService)
    {
        _replyService = replyService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ReplyViewModel>), 200)]
    public async Task<IActionResult> GetAllAsync([FromRoute]long ticketId, CancellationToken cancellationToken = default)
    {
        var response = await _replyService.GetAllAsync(ticketId, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(void), 201)]
    public async Task<IActionResult> PostAsync([FromRoute]long ticketId, [FromBody]ReplyPostModel request, CancellationToken cancellationToken = default)
    {
        await _replyService.PostAsync(ticketId, request, cancellationToken);
        return Created();
    }
}
