using BeykentLibraryAPP.Models.BookViewModels;
using BusinessLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BeykentLibraryAPP.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly LibraryService _libraryService;

        public AdminController(LibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        public IActionResult Index()
        {

            return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _libraryService.GetBooksAsync();

            var bookInfoList = books.Select(book => new BookInfoViewModel
            {
                BookId = book.BookId,
                Title = book.Title,
                DailyRentalRate = book.DailyRentalRate
            }).ToList();

            var jsonResult = JsonConvert.SerializeObject(bookInfoList);

            return Content(jsonResult, "application/json");
        }



    }
}
