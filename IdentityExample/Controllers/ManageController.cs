using IdentityExample.Models;
using IdentityExample.Models.ManageViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IdentityExample.Controllers
{
    public class ManageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ManageController(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                throw new ApplicationException($"Could not load user with ID: {_userManager.GetUserId(User)}");
            }

            var model = new IndexViewModel
            {
                Username = user.UserName,
                EmailConfirmed = user.EmailConfirmed,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                StatusMessage = StatusMessage
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                throw new ApplicationException($"Could not load user with ID: {_userManager.GetUserId(User)}");
            }

            if (user.Email != model.Email)
            {
                var result = await _userManager.SetEmailAsync(user, model.Email);

                if (!result.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error assigning email to user with ID: {user.Id}");
                }
            }

            if (user.PhoneNumber != model.PhoneNumber)
            {
                var result = await _userManager.SetPhoneNumberAsync(user, model.PhoneNumber);

                if (!result.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error assigning phone number to user with ID: {user.Id}");
                }
            }

            StatusMessage = "Your profile has been updated successfully!";

            return RedirectToAction(nameof(Index));
        }
    }
}
