using Microsoft.AspNetCore.Mvc;

namespace TBot.WebApp.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class HomeController: ControllerBase
{
    [HttpGet]
    public string Index()
    {
        return "api/Home/Index";
    }
}