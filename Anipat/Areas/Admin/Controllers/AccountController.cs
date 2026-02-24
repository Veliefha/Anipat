using Anipat.Models;
using Anipat.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Anipat.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            AppUser newUser = new AppUser
            {
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Email = registerVM.Email,
                UserName = registerVM.Email
            };

            var result = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (result.Succeeded)
            {
                // Hər ehtimala qarşı rollar bazada yoxdursa yaradırıq
                if (!await _roleManager.RoleExistsAsync("Admin"))
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                if (!await _roleManager.RoleExistsAsync("User"))
                    await _roleManager.CreateAsync(new IdentityRole("User"));

                // DİQQƏT: Əgər bazada ilk istifadəçisənsə, səni Admin edir
                if (_userManager.Users.Count() == 1)
                {
                    await _userManager.AddToRoleAsync(newUser, "Admin");
                }
                else
                {
                    await _userManager.AddToRoleAsync(newUser, "User");
                }

                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
            return View(registerVM);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "E-mail və ya parol səhvdir!");
                return View(loginVM);
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, true);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "E-mail və ya parol səhvdir!");
                return View(loginVM);
            }

            // Əgər adminsənsə birbaşa Admin Panelə, deyilsənsə ana səhifəyə
            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                return RedirectToAction("Index", "Pet"); // Məsələn, Pet siyahısına
            }

            return Redirect("/"); // Normal user üçün ana səhifə
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}