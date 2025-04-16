using Demo.DAL.Models.IdentityModel;
using Demo.presentation.ViewModels.User;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.presentation.Controllers
{
    public class UserController(UserManager<ApplicationUser> _userManger,
                                IWebHostEnvironment _webHostEnvironment,
                                ILogger<UserController> _logger) : Controller
    {
        public IActionResult Index(string UserSearch)
        {
            var users = _userManger.Users;

            if (!string.IsNullOrWhiteSpace(UserSearch))
            {
                users = users.Where(u =>
                    u.FirstName.Contains(UserSearch) ||
                    u.LastName.Contains(UserSearch));
            }

            return View(users.ToList());
        }

        #region Details
        [HttpGet]
        public IActionResult Details(string id)
        {
            var user = _userManger.Users.FirstOrDefault(x => x.Id == id);
            if (user is not null)
            {
                var UserDetailsDto = new UserDetailsDto()
                {
                    Id = user.Id,
                    FName = user.FirstName,
                    LName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                };
                return View(UserDetailsDto);
            }
            return NotFound();
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var User = _userManger.Users.FirstOrDefault(x => x.Id == id);
            if (User is not null)
            {
                var UserDetailsDto = new UpdatedUserDto()
                {
                    Id = User.Id,
                    FName = User.FirstName,
                    LName = User.LastName,
                    PhoneNumber = User.PhoneNumber
                };
                return View(UserDetailsDto);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(UpdatedUserDto updatedUserDto)
        {
            if (ModelState.IsValid)
            {
                var user = _userManger.Users.FirstOrDefault(x => x.Id == updatedUserDto.Id);
                if (user is not null)
                {
                    user.FirstName = updatedUserDto.FName;
                    user.LastName = updatedUserDto.LName;
                    user.PhoneNumber = updatedUserDto.PhoneNumber;
                    var result = _userManger.UpdateAsync(user).Result;
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(updatedUserDto);
        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(string id)
        {
            var user = _userManger.Users.FirstOrDefault(x => x.Id == id);
            if (user is not null)
            {
                var UserDetailsDto = new UserDetailsDto()
                {
                    Id = user.Id,
                    FName = user.FirstName,
                    LName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                };
                return View(UserDetailsDto);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult ConfirmDelete(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return BadRequest();
            try
            {
                var user = _userManger.Users.FirstOrDefault(x => x.Id == id);
                if (user is not null)
                {
                    var result = _userManger.DeleteAsync(user).Result;
                    
                    if (result.Succeeded)
                    {
                        TempData["SuccessMessage"] = "User deleted successfully!";
                        return RedirectToAction("Index"); 
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Failed to delete User");
                    return RedirectToAction(nameof(Index), new { id = id });
                }

            }
            catch(Exception ex)
            {
                //ModelState.AddModelError("", ex.Message);
                if (_webHostEnvironment.IsDevelopment())
                {
                    //1- Devolpment=> log error in console and return error message to user
                    //ModelState.AddModelError("", ex.Message);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //2-Deployment=> log error in file or database and return  error view,
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }
            return NotFound();
        }

        #endregion

    }
}
