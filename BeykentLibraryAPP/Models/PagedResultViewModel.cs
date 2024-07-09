using BeykentLibraryAPP.Models.BookViewModels;

namespace BeykentLibraryAPP.Models
{
    public class PagedResultViewModel
    {
        public List<ResultBookViewModel> Books { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}
