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

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateTeam([FromBody] CreateTeamCommand command)
    {
        var result = await _sender.Send(command);
        return Ok(result);
    }
}