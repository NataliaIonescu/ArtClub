using Microsoft.AspNetCore.Mvc;
using ArtClub.Models;
using Microsoft.AspNetCore.Identity;

namespace ArtClub.Controllers
{
    public class RegisterControllerMember : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public RegisterControllerMember(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: RegisterControllerMember
        public IActionResult Index()
        {
            return View();
        }

        // GET: RegisterControllerMember/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RegisterControllerMember/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create a new ApplicationUser object to store the user's data
                var user = new User
                {
                    Email = model.Email,
                    Username = model.Username,
                };

                // Attempt to create a new user with the provided data
                var result = _userManager.CreateAsync(user, model.Password).Result;

                if (result.Succeeded)
                {
                    // Add the new user to the specified role
                    _userManager.AddToRoleAsync(user, model.Role).Wait();

                    // Sign in the newly created user and redirect to the homepage
                    _signInManager.SignInAsync(user, isPersistent: false).Wait();
                    return RedirectToAction("Index", "Home");
                }

                // If the creation was not successful, add any errors to the ModelState
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we get to this point, the ModelState is invalid, so return the registration view with the provided model
            return View(model);
        }
    }
}