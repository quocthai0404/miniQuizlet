using Microsoft.AspNetCore.Mvc;

namespace miniQuizlet.Controllers;

public class LoginController : Controller
{
	public IActionResult Index()
	{
		return View();
	}
}
