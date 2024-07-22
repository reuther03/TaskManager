using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Modules.Management.Api.Controllers;

[ApiController]
[Route(ManagementModule.BasePath + "/[controller]")]
internal abstract class BaseController : ControllerBase
{

}