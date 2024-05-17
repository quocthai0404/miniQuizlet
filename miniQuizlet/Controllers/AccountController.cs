using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using miniQuizlet.Models;
using miniQuizlet.Services;
using miniQuizlet.ViewModel_DTO;
using System.Diagnostics;

namespace miniQuizlet.Controllers;
[Route("account")]
public class AccountController : Controller
{
    private IAccountService accountService;
    public AccountController(IAccountService _accountService)
    {
        accountService = _accountService;
    }
    [Route("~/")]
    [Route("")]
    [Route("Index")]
    public IActionResult Index()
    {
        return View("login");
    }
    [Route("signup")]
    public IActionResult SignUp()
    {
        var user = new UserFormBinding()
        {
            Gender = false,
            BirthDay = DateTime.Now,
        };
        return View("signup", user);
    }
    [HttpPost]
    [Route("signup")]
    public IActionResult SignUp(UserFormBinding userFormBinding, string genderPost)
    {
        userFormBinding.Gender = genderPost == "true";
        if (accountService.existEmail(userFormBinding.Email))
        {
            TempData["Msg"] = "da co";
            return RedirectToAction("signup");
        }
        else {
            TempData["Msg"] = "chua co";
            return RedirectToAction("signup");
        }
        if (!userFormBinding.Agree)
        {
            TempData["Msg"] = "Use must accept our Terms of service";
            return RedirectToAction("signup");
        }

        if (userFormBinding.Password != userFormBinding.Repassword)
        {
            TempData["Msg"] = "Repassword Not Match";
            return RedirectToAction("signup");
        }
        if (string.IsNullOrEmpty(userFormBinding.Fullname) || string.IsNullOrEmpty(userFormBinding.Email))
        {
            TempData["Msg"] = "Some Field Is Null";
            return RedirectToAction("signup");
        }
        //if (accountService.signUp(new User()
        //{
        //    Email = userFormBinding.Email,
        //    Password = BCrypt.Net.BCrypt.HashPassword(userFormBinding.Password),
        //    Fullname = userFormBinding.Fullname,
        //    BirthDay = userFormBinding.BirthDay,
        //    Gender = userFormBinding.Gender
        //}))
        //{
        //    // send mail

        //}
        //else {
        //    TempData["Msg"] = "failed registration";
        //    return RedirectToAction("signup");
        //}
        return View("login");
    }
}

