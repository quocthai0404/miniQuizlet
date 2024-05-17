using Microsoft.AspNetCore.Mvc;

namespace miniQuizlet.Controllers;
[Route("l")]
public class SharedController : Controller
{
    [Route("i")]
    public IActionResult Index()
    {
        return View("_Layout");
    }
}
