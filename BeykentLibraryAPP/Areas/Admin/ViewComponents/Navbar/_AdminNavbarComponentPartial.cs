using Microsoft.AspNetCore.Mvc;

namespace BeykentLibraryAPP.Areas.Admin.ViewComponents.Navbar
{
    public class _AdminNavbarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
