using Microsoft.AspNetCore.Mvc;
using EA_MVC_Practice.DTO;
using EA_MVC_Practice.Interfaces;
namespace EA_MVC_Practice.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService) 
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {

            return View(viewName: "Register", new RegisterUserDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(viewName: "Register", model);
            }

            var result = await _accountService.RegisterUserAsync(model);

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            return View();

        }

        [HttpGet]

        public async Task<IActionResult> Login()
        {
            return View(viewName: "Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(viewName: "Login", model);
            }

            try
            {
                await _accountService.LoginAsync(model);
                return RedirectToAction("Index", "Home");
            }catch(Exception ex)
            {
                ViewData["Error"] = ex.Message;
            }

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return View("Login");
        }

        [HttpGet]
        public async Task<IActionResult> AccessDenied()
        {
            return View(viewName: "AccessDenied");
        }
        
    }
}
