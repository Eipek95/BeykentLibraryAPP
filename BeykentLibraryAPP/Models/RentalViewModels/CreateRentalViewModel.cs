namespace BeykentLibraryAPP.Models.RentalViewModels
{
    public class CreateRentalViewModel
    {
        public string UserId { get; set; }
        public int BookId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal TotalCost { get; set; }
    }
}
