using Microsoft.AspNetCore.Mvc;

namespace BeykentLibraryAPP.Areas.Admin.ViewComponents.Script
{
    public class _AdminScriptComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
