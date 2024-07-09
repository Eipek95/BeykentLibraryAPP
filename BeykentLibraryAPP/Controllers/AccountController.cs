using BeykentLibraryAPP.Extensions;
using BeykentLibraryAPP.Models;
using EntityLayer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeykentLibraryAPP.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            returnUrl = string.IsNullOrEmpty(returnUrl) ? Url.Action("Index", "Home") : returnUrl;
            var hasUser = await _userManager.FindByEmailAsync(loginViewModel.Mail);
            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Email veya Şifre Yanlış!");
                return View();
            }
            var signInResult = await _signInManager.PasswordSignInAsync(hasUser!, loginViewModel.Password, loginViewModel.RememberMe, false);
            if (signInResult.Succeeded)
            {

                return Redirect(returnUrl!);
            }

            ModelState.AddModelErrorList(new List<string> { "Email veya şifre Yanlış" });
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var appUser = new AppUser
            {
                Email = registerViewModel.Mail,
                UserName = registerViewModel.Username
            };
            var result = await _userManager.CreateAsync(appUser, registerViewModel.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(appUser, "User");
                TempData["SuccessMessage"] = "Üyelik kayıt işlemi başarıyla gerçekleşmiştir.";
                return RedirectToAction(nameof(AccountController.Login), "Account");
            }

            ModelState.AddModelErrorList(result.Errors);
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult PasswordChange()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PasswordChange(PasswordChangeVewModel request)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Şifreler Uyuşmuyor");
                return View();
            }

            var currentUser = await _userManager.FindByNameAsync(User.Identity!.Name!);

            var checkOldPassword = await _userManager.CheckPasswordAsync(currentUser, request.PasswordOld);

            if (!checkOldPassword)
            {
                ModelState.AddModelError(string.Empty, "Eski Şifreniz Yanlış");
                return View();
            }

            var resultChangePassword = await _userManager.ChangePasswordAsync(currentUser, request.PasswordOld, request.PasswordNew);
            if (!resultChangePassword.Succeeded)
            {
                ModelState.AddModelErrorList(resultChangePassword.Errors);
            }

            await _userManager.UpdateSecurityStampAsync(currentUser);
            TempData["SuccessMessage"] = "Şifreniz Başarıyla Değiştirilmiştir.Lütfen Tekrardan Giriş Yapınız";
            return RedirectToAction(nameof(AccountController.Logout), new { returnurl = "/Admin/Account/Login" });
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
