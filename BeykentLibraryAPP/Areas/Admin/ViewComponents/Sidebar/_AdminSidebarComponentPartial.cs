using Microsoft.AspNetCore.Mvc;

namespace BeykentLibraryAPP.Areas.Admin.ViewComponents.Sidebar
{
    public class _AdminSidebarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
