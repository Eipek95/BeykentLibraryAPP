using BeykentLibraryAPP.Models.BookViewModels;
using BusinessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BeykentLibraryAPP.Areas.Admin.ViewComponents.AdminSection
{
    public class _AdminLastAddFiveBookComponentPartial : ViewComponent
    {
        private readonly LibraryService _libraryService;

        public _AdminLastAddFiveBookComponentPartial(LibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var books = await _libraryService.GetBooksAsync();

            var booksvm = books.Select(x => new ResultBookViewModel
            {
                Author = x.Author,
                BookId = x.BookId,
                CoverImage = x.CoverImage,
                DailyRentalRate = x.DailyRentalRate,
                Description = x.Description,
                Title = x.Title,
            }).TakeLast(5).ToList();
            return View(booksvm);
        }
    }
}
