using Microsoft.AspNetCore.Mvc;
using Neosoft_LeaveManagement.Constants;
using Neosoft_LeaveManagement.Interfaces;
using Neosoft_LeaveManagement.Models;
using Neosoft_LeaveManagement.ViewModels;

namespace Neosoft_LeaveManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Register() => View();
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                string profilePicturePath = null;

                if (model.ProfilePicture != null && model.ProfilePicture.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    Directory.CreateDirectory(uploadsFolder);

                    string uniqueFileName = $"{Guid.NewGuid()}_{model.ProfilePicture.FileName}";
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ProfilePicture.CopyToAsync(stream);
                    }

                    profilePicturePath = "/uploads/" + uniqueFileName;
                }

                var newUser = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password,
                    Role = UserRole.Employee,
                    ProfilePicture = profilePicturePath 
                };

                await _userService.Register(newUser, UserRole.Employee);

                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }


        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var user = await _userService.Login(model.Email, model.Password);
                if (user != null)
                {
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    HttpContext.Session.SetString("Name", user.Name);
                    HttpContext.Session.SetInt32("UserRole", (int)user.Role);
                    if (!string.IsNullOrEmpty(user.ProfilePicture))
                    {
                        HttpContext.Session.SetString("ProfilePicture", user.ProfilePicture);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password");
                    return View(model);
                }

                
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
        public async Task<IActionResult> Profile()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Login");

        var user = await _userService.GetUserByIdAsync(userId.Value);
        if (user == null)
            return NotFound();

        return View(user);
    }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
