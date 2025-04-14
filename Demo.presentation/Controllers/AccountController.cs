using Demo.DAL.Models.IdentityModel;
using Demo.presentation.ViewModels.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.presentation.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager,
                                    SignInManager<ApplicationUser> _signInManager) : Controller
    {
        #region Register
        [HttpGet]
        public IActionResult Register() =>  View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            var User = new ApplicationUser
            {
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email,
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName
            };

            var Result = _userManager.CreateAsync(User, registerViewModel.Password).Result;

            if(Result.Succeeded)
                return RedirectToAction("Login", "Account");
            else
            {
                foreach (var error in Result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerViewModel);
            }
        }
        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login () => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(loginViewModel.Email).Result;   
                if(user is not null)
                {
                    var flag = _userManager.CheckPasswordAsync(user, loginViewModel.Password).Result;
                    if (flag)
                    {
                        var result = _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false).Result;
                        if (result.Succeeded)
                            return RedirectToAction(nameof(HomeController.Index), "Home");

                    }
                }
                ModelState.AddModelError("", "Invalid login!!!");
            }
            return View(loginViewModel);
        }
        #endregion
    }
}
