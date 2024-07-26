using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Modules.Management.Application.Features.Commands.Teams;

namespace TaskManager.Modules.Management.Api.Controllers;

internal class ManagementsController : BaseController
{
    private readonly ISender _sender;

    public ManagementsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("create-team")]
    [Authorize]
    public async Task<IActionResult> CreateTeam([FromBody] CreateTeamCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpPost("add-team-member")]
    [Authorize]
    public async Task<IActionResult> AddTeamMember([FromBody] AddTeamMemberCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpPost("add-task")]
    [Authorize]
    public async Task<IActionResult> AddTask([FromBody] AddTaskCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }
}