using Demo.DAL.Models.IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Demo.presentation.Controllers
{
    [Authorize]
    public class RoleController(RoleManager<IdentityRole> _roleManager,
                                IWebHostEnvironment _environment,
                                ILogger<RoleController> _logger) : Controller
    {
        public IActionResult Index()
        {
            var Roles = _roleManager.Roles.ToList();
            return View(Roles);
        }

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                ModelState.AddModelError("", "Role name is required");
                return View();
            }
            var role = new IdentityRole(roleName);
            var result = _roleManager.CreateAsync(role).Result;
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Role created successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();      
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role == null) return NotFound();
            return View(role);
        }

        [HttpPost]
        public IActionResult Edit(IdentityRole identityRole)
        {
            try
            {
                if (!ModelState.IsValid) return View(identityRole);
                var role = _roleManager.FindByIdAsync(identityRole.Id).Result;
                if (role == null)
                {
                    TempData["ErrorMessage"] = "Role not found.";
                    return RedirectToAction("Index");
                }

                role.Name = identityRole.Name;
                var result = _roleManager.UpdateAsync(role).Result;
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Role updated successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                return View();
            }
            catch (Exception ex)
            {

                if(_environment.IsDevelopment())
                {
                    //1- Devolpment=> log error in console and return error message to user
                    _logger.LogError(ex, "Error occurred while editing role");
                    ModelState.AddModelError("", ex.Message);
                }
                else
                {
                    //2- Production=> log error in file and return generic error message to user
                    _logger.LogError(ex, "Error occurred while editing role");
                    ModelState.AddModelError("", "An error occurred while editing the role");
                }
            }
            return View(identityRole);
        }

        #endregion

        #region Details
        public IActionResult Details(string id)
        {
            var Role = _roleManager.Roles.FirstAsync(x => x.Id == id).Result;
            return View(Role);
        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(string id)
        {
            var Role = _roleManager.FindByIdAsync(id).Result;
            return View(Role);
        }

        [HttpPost]
        public IActionResult Delete(IdentityRole identityRole)
        {
            var role = _roleManager.FindByIdAsync(identityRole.Id).Result;
            if (role == null)
            {
                TempData["ErrorMessage"] = "Role not found.";
                return RedirectToAction("Index");
            }
            var result = _roleManager.DeleteAsync(role).Result;
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Role deleted successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
        #endregion


    }
}
