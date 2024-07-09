namespace BeykentLibraryAPP.Models.RentalViewModels
{
    public class ResultRentalViewModel
    {
        public int RentalId { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal TotalCost { get; set; }
        public string BookName { get; set; }
    }
}
