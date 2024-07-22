using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Modules.Management.Api.Controllers;

[ApiController]
[Route(ManagementModule.BasePath + "/[controller]")]
internal class HomeController : BaseController
{
    [HttpGet]
    public ActionResult<string> Get() => Ok("TaskManager.Modules.Management.Api");
}