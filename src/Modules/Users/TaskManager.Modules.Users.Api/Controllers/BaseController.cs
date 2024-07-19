using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Modules.Users.Api.Controllers;

[ApiController]
[Route(UsersModule.BasePath + "/[controller]")]
internal abstract class BaseController : ControllerBase
{
}