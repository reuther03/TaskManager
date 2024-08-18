using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Modules.Management.Application.Features.Commands.Teams;
using TaskManager.Modules.Management.Application.Features.Commands.Teams.Files;
using TaskManager.Modules.Management.Application.Features.Queries;

namespace TaskManager.Modules.Management.Api.Controllers;

internal class ManagementsController : BaseController
{
    private readonly ISender _sender;

    public ManagementsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("team/{teamId:guid}")]
    [Authorize]
    public async Task<IActionResult> GetTeams([FromRoute] Guid teamId)
    {
        var result = await _sender.Send(new GetTeamQuery(teamId));
        return Ok(result);
    }

    [HttpGet("teams")]
    [Authorize]
    public async Task<IActionResult> GetTeams([FromQuery] GetMemberTeamsQuery query)
    {
        var result = await _sender.Send(query);
        return Ok(result);
    }

    [HttpGet("teams/search")]
    [Authorize]
    public async Task<IActionResult> GetFilteredTeams([FromQuery] GetTeamsBySearchValue query)
    {
        var result = await _sender.Send(query);
        return Ok(result);
    }


    [HttpPost("create-team")]
    [Authorize]
    public async Task<IActionResult> CreateTeam([FromBody] CreateTeamCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpPost("{teamId:Guid}/add-team-member")]
    [Authorize]
    public async Task<IActionResult> AddTeamMember([FromRoute] Guid teamId, [FromBody] AddTeamMemberCommand command)
    {
        var result = await _sender.Send(command with { UsersTeamId = teamId });
        return Ok(result);
    }

    [HttpPost("{teamId:guid}/add-task")]
    [Authorize]
    public async Task<IActionResult> AddTask([FromRoute] Guid teamId, [FromBody] AddTaskCommand command)
    {
        var result = await _sender.Send(command with { CurrentTeamId = teamId });
        return Ok(result);
    }

    [HttpPost("{teamId:guid}/upload-file")]
    [Authorize]
    public async Task<IActionResult> UploadFile([FromRoute] Guid teamId, [FromForm] UploadFileCommand command)
    {
        var result = await _sender.Send(command with { TeamId = teamId });
        return Ok(result);
    }

    [HttpPatch("change-member-role")]
    [Authorize]
    public async Task<IActionResult> ChangeMemberRole([FromBody] ChangeMemberRoleCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }

    [HttpDelete("{teamId:guid}/delete-team")]
    [Authorize]
    public async Task<IActionResult> DeleteTeam([FromRoute] Guid teamId)
    {
        var result = await _sender.Send(new DeleteTeamCommand(teamId));
        return Ok(result);
    }

    [HttpDelete("{teamId:guid}/delete-file/{fileId:guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteFile([FromRoute] Guid teamId, [FromRoute] Guid fileId)
    {
        var result = await _sender.Send(new DeleteFileCommand(fileId, teamId));
        return Ok(result);
    }
}