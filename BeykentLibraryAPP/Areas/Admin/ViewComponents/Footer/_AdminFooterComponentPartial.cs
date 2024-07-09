using Microsoft.AspNetCore.Mvc;

namespace BeykentLibraryAPP.Areas.Admin.ViewComponents.Footer
{
    public class _AdminFooterComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
