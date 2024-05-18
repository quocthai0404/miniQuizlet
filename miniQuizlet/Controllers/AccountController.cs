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
    private IMailService mailService;
    private IConfiguration configuration;
    public AccountController(IAccountService _accountService, IMailService _mailService, IConfiguration _configuration)
    {
        accountService = _accountService;
        mailService = _mailService;
        configuration = _configuration;
    }
    [Route("~/")]
    [Route("")]
    [Route("Index")]
    public IActionResult Index()
    {
        return View("login");
    }

    [Route("thankyou")]
    public IActionResult thanks()
    {
        return View("thankyou");
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
        string url = configuration["BaseUrl"];
        userFormBinding.Gender = genderPost == "true";
        var nameCheck = userFormBinding.Email.Split("@")[0];

        string securityCode = Helpers.RandomHelper.generateSecurityCode();
        var mail = new Mail(url+ "account/active?securityCode=" + securityCode+"&check="+ nameCheck);

        if (accountService.existEmail(userFormBinding.Email))
        {
            TempData["Msg"] = "This email is used";
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

        User user = new User()
        {
            Email = userFormBinding.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(userFormBinding.Password),
            Fullname = userFormBinding.Fullname,
            BirthDay = userFormBinding.BirthDay,
            Gender = userFormBinding.Gender
        };
        if (!accountService.addActiveAccount(new ActiveAccount() { Email = user.Email, Active = false, Expired = DateTime.Now.AddMinutes(15), SecurityCode = securityCode }))
        {
            TempData["Msg"] = "failed";
            return RedirectToAction("signup");
        }
        if (accountService.signUp(user))
        {
            if (mailService.Send("maillaravel1508@gmail.com", userFormBinding.Email, "Please Active Your Email", mail.Email))
            {
                return View("thankyou");
            }
            else
            {
                TempData["Msg"] = "failed to send email";
                return RedirectToAction("signup");
            }

        }
        else
        {
            TempData["Msg"] = "failed registration";
            return RedirectToAction("signup");
        }

    }


    [HttpGet]
    [Route("active")]
    public IActionResult active(string securityCode, string check) { 
        var accountActive = accountService.getActiveAccountByCode(securityCode);
        if (accountActive == null) {
            TempData["Msg"] = "failed to active account";
            return RedirectToAction("signup");
        }
        var nameCheck = accountActive.Email.Split("@")[0];
        if (accountActive.SecurityCode == securityCode && nameCheck == check && accountActive.Active ==false && accountActive.Expired < DateTime.Now)
        {
            if (!accountService.removeActiveAccount(accountActive)) {
                TempData["Msg"] = "active account fail";
                return RedirectToAction("Index");
            }
            TempData["Msg"] = "active account success";
            return RedirectToAction("Index");
        }
        TempData["Msg"] = "active failed";
        return RedirectToAction("Index");
    }

    
}

