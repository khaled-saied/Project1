using Demo.DAL.Models.IdentityModel;
using Demo.presentation.Helper;
using Demo.presentation.Utilities;
using Demo.presentation.ViewModels.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.presentation.Controllers
{
    //[Authorize]
    public class AccountController(UserManager<ApplicationUser> _userManager,
                                    SignInManager<ApplicationUser> _signInManager,
                                    IMailService _mailService,
                                    ISmsService _smsService) : Controller
    {
        #region Register
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var existingUser =  _userManager.Users.FirstOrDefaultAsync(u => u.Email == registerViewModel.Email).Result;

                if (existingUser != null)
                {
                    TempData["EmailExists"] = "This Email is already used!!";
                    return View(registerViewModel);
                }

                var User = new ApplicationUser
                {
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.Email,
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName
                };

                var Result = _userManager.CreateAsync(User, registerViewModel.Password).Result;

                if (Result.Succeeded)
                    return RedirectToAction("Login", "Account");
                else
                {
                    foreach (var error in Result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
                    return View(registerViewModel);
        }
        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(loginViewModel.Email).Result;
                if (user is not null)
                {
                    var flag = _userManager.CheckPasswordAsync(user, loginViewModel.Password).Result;
                    if (flag)
                    {
                        var result = _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false).Result;
                        if (result.Succeeded)
                            return RedirectToAction("Index","Home");

                    }
                }
                ModelState.AddModelError("", "Invalid login!!!");
            }
            return View(loginViewModel);
        }

        //Login with Google
        public IActionResult GoogleLogin()
        {
            var Prop = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse")
            };

            return Challenge(Prop, GoogleDefaults.AuthenticationScheme);

        }


        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

            var Claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value

            });

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region SignOut
        [HttpGet]
        public IActionResult SignOut()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction(nameof(Login));
        }
        #endregion

        #region Forget Password
        [HttpGet]
        public IActionResult ForgetPassword() => View();


        //Mail
        [HttpPost]
        public IActionResult SendResetPasswordLink(ForgetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var User = _userManager.FindByEmailAsync(viewModel.Email).Result;
                if (User is not null)
                {
                    // Generate Token
                    var Token = _userManager.GeneratePasswordResetTokenAsync(User).Result;
                    var ResetPasswordLink = Url.Action("ResetPassword", "Account", new { email = viewModel.Email,Token }, Request.Scheme);
                    var email = new Email
                    {
                        To = viewModel.Email,
                        Subject = "Reset Password",
                        Body = ResetPasswordLink
                    };

                    // Send Email
                    //EmailSettings.SendEmail(email);
                    _mailService.Send(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
            }
            ModelState.AddModelError("", "Invalid Email");
            return View("ForgetPassword", viewModel);
        }

        //Sms
        [HttpPost]
        public IActionResult SendResetPasswordLinkSms(ForgetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var User = _userManager.FindByEmailAsync(viewModel.Email).Result;
                if (User is not null)
                {
                    // Generate Token
                    var Token = _userManager.GeneratePasswordResetTokenAsync(User).Result;
                    var ResetPasswordLink = Url.Action("ResetPassword", "Account", new { email = viewModel.Email, Token }, Request.Scheme);
                   
                    var SmsMessage = new SmsMessage
                    {
                        PhoneNumber = User.PhoneNumber,
                        Body = ResetPasswordLink
                    };

                    // Send Sms
                    _smsService.SendSms(SmsMessage);
                    return Ok("Sms Sent Successfully");
                }
            }
            ModelState.AddModelError("", "Invalid Email");
            return View("ForgetPassword", viewModel);
        }


        public IActionResult CheckYourInbox()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string Token)
        {
            TempData["email"] = email;
            TempData["Token"] = Token;
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel passwordViewModel)
        {
            if (!ModelState.IsValid)
                return View(passwordViewModel);

            string email = TempData["email"] as string ?? string.Empty;
            string token = TempData["Token"] as string ?? string.Empty;
            var User = _userManager.FindByEmailAsync(email).Result;
            if (User is not null)
            {
                var result = _userManager.ResetPasswordAsync(User, token, passwordViewModel.Password).Result;
                if (result.Succeeded)
                    return RedirectToAction("Login");
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(nameof(ResetPassword), passwordViewModel);
        }
        #endregion

    }
}
