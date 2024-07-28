using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Modules.Management.Application.Features.Commands.Tasks;

namespace TaskManager.Modules.Management.Api.Controllers;

internal class TasksController : BaseController
{
    private readonly ISender _sender;

    public TasksController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPatch("{teamId:guid}/progress")]
    [Authorize]
    public async Task<IActionResult> ChangeTaskProgress([FromRoute] Guid teamId, [FromBody] ChangeTaskStatusCommand command)
    {
        var result = await _sender.Send(command with { CurrentTeamId = teamId });
        return Ok(result);
    }
}