using BusinessLayer.Concrete;
using EntityLayer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeykentLibraryAPP.Areas.Admin.ViewComponents.AdminSection
{
    public class _AdminStatisticsComponentPartial : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly LibraryService _libraryService;

        public _AdminStatisticsComponentPartial(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, LibraryService libraryService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _libraryService = libraryService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var books = await _libraryService.GetBooksAsync();
            var adminRole = await _roleManager.FindByNameAsync("admin");
            var adminUsers = await _userManager.GetUsersInRoleAsync(adminRole.Name);
            var totalUsers = await _userManager.Users.CountAsync();
            var nonAdminUsersCount = totalUsers - adminUsers.Count;

            ViewBag.UserCount = nonAdminUsersCount;
            ViewBag.AdminCount = adminUsers.Count;
            ViewBag.BookCount = books.Count;

            return View();
        }
    }
}
