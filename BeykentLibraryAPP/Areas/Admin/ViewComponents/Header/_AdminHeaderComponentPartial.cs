using Microsoft.AspNetCore.Mvc;

namespace BeykentLibraryAPP.Areas.Admin.ViewComponents.Header
{
    public class _AdminHeaderComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
