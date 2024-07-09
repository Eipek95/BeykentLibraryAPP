namespace EntityLayer.Model
{
    public class Rental
    {
        public int RentalId { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal TotalCost { get; set; }

        public AppUser User { get; set; }
        public Book Book { get; set; }
    }
}
