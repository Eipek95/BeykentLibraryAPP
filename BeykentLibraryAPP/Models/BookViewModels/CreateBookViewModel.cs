namespace BeykentLibraryAPP.Models.BookViewModels
{
    public class CreateBookViewModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string CoverImage { get; set; }
        public string? Description { get; set; }
        public decimal DailyRentalRate { get; set; }
    }
}
