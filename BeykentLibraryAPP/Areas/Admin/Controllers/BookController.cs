using BeykentLibraryAPP.Models.BookViewModels;
using BusinessLayer.Concrete;
using EntityLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeykentLibraryAPP.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class BookController : Controller
    {
        private readonly LibraryService _libraryService;

        public BookController(LibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _libraryService.GetBooksAsync();
            var result = books.Select(x => new ResultBookViewModel
            {
                Author = x.Author,
                Title = x.Title,
                BookId = x.BookId,
                CoverImage = x.CoverImage,
                DailyRentalRate = x.DailyRentalRate,
                Description = x.Description,
            }).ToList();
            return View(result);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateBookViewModel model)
        {
            await _libraryService.AddBookAsync(new Book
            {
                Author = model.Author,
                Title = model.Title,
                CoverImage = model.CoverImage,
                DailyRentalRate = model.DailyRentalRate,
                Description = model.Description,

            });
            return Redirect("/Admin/Book/Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var book = await _libraryService.GetBookByIdAsync(id);
            return View(new UpdateBookViewModel
            {
                Author = book.Author,
                Title = book.Title,
                BookId = book.BookId,
                CoverImage = book.CoverImage,
                DailyRentalRate = book.DailyRentalRate,
                Description = book.Description,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateBookViewModel model)
        {

            await _libraryService.UpdateBookAsync(new Book
            {
                BookId = model.BookId,
                Author = model.Author,
                Title = model.Title,
                CoverImage = model.CoverImage,
                DailyRentalRate = model.DailyRentalRate,
                Description = model.Description,

            });
            return Redirect("/Admin/Book/Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _libraryService.DeleteBookAsync(id);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }
    }
}
