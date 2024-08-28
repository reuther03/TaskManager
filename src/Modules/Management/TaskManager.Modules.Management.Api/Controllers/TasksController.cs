using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Modules.Management.Application.Features.Commands.Tasks;
using TaskManager.Modules.Management.Application.Features.Queries;
using TaskManager.Modules.Management.Domain.TaskItems;

namespace TaskManager.Modules.Management.Api.Controllers;

[Route("{teamId:guid}")]
internal class TasksController : BaseController
{
    private readonly ISender _sender;

    public TasksController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("tasks")]
    [Authorize]
    public async Task<IActionResult> GetTasks([FromQuery] GetTeamTasksByStatusQuery query, [FromRoute] Guid teamId)
    {
        var result = await _sender.Send(query with { TeamId = teamId });
        return Ok(result);
    }


    [HttpPatch("progress")]
    [Authorize]
    public async Task<IActionResult> ChangeTaskProgress([FromRoute] Guid teamId, [FromBody] ChangeTaskStatusCommand command)
    {
        var result = await _sender.Send(command with { CurrentTeamId = teamId });
        return Ok(result);
    }

    [HttpPost("tasks/{taskId:guid}/subtasks")]
    [Authorize]
    public async Task<IActionResult> AddSubTask([FromRoute] Guid teamId, [FromRoute] Guid taskId, [FromBody] AddSubTaskCommand command)
    {
        var result = await _sender.Send(command with { TeamId = teamId, TaskId = taskId });
        return Ok(result);
    }

    [HttpDelete("tasks/{taskId:guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteTask([FromRoute] Guid teamId, [FromRoute] Guid taskId)
    {
        var result = await _sender.Send(new DeleteTaskCommand(taskId, teamId));
        return Ok(result);
    }
}