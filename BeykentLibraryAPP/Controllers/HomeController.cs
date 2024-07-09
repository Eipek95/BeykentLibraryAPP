using BeykentLibraryAPP.Models;
using BeykentLibraryAPP.Models.BookViewModels;
using BeykentLibraryAPP.Models.RentalViewModels;
using BusinessLayer.Concrete;
using EntityLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BeykentLibraryAPP.Controllers
{
    [Authorize(Roles = "admin,user")]
    public class HomeController : Controller
    {
        private readonly LibraryService _libraryService;
        public HomeController(LibraryService libraryService)
        {
            _libraryService = libraryService;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 8)
        {
            var books = await _libraryService.GetBooksAsync();
            var pagedBooks = PagedList<Book>.Create(books, pageIndex, pageSize);

            var viewModel = new PagedResultViewModel
            {
                Books = pagedBooks.Select(x => new ResultBookViewModel
                {
                    Author = x.Author,
                    BookId = x.BookId,
                    CoverImage = x.CoverImage,
                    DailyRentalRate = x.DailyRentalRate,
                    Description = x.Description,
                    Title = x.Title,
                }).ToList(),
                PageIndex = pagedBooks.PageIndex,
                TotalPages = pagedBooks.TotalPages,
                HasPreviousPage = pagedBooks.HasPreviousPage,
                HasNextPage = pagedBooks.HasNextPage
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> RentBook(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _libraryService.RentBookAsync(userId: userId!, bookId: id, rentalDate: DateTime.Now);
            return Json(new { success = true, message = "Kitap kiralandý!" });
        }


        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GetMyRentBook()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var books = await _libraryService.GetRentalsByUserIdAsync(userId!);

            return View(books.Select(b => new ResultRentalViewModel
            {
                BookId = b.BookId,
                RentalDate = DateTime.Now,
                RentalId = b.RentalId,
                ReturnDate = b.ReturnDate,
                TotalCost = b.TotalCost,
                BookName = b.Book.Title,
            }).ToList());
        }
        [HttpPost]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> ReturnBook(int rentalId)
        {

            await _libraryService.ReturnBookAsync(rentalId);
            return Json(new { success = true, message = "Kitap kiralandý!" });
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
