namespace BeykentLibraryAPP.Models.BookViewModels
{
    public class ResultBookViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string CoverImage { get; set; }
        public string? Description { get; set; }
        public decimal DailyRentalRate { get; set; }
    }
}
