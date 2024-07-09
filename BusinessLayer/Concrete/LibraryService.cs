using EntityLayer.Model;
using Repositories.Concrete;

namespace BusinessLayer.Concrete
{
    public class LibraryService
    {
        private readonly LibraryRepository _repository;

        public LibraryService(LibraryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            return await _repository.GetBooksAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _repository.GetBookByIdAsync(id);
        }

        public async Task AddBookAsync(Book book)
        {
            await _repository.AddBookAsync(book);
        }

        public async Task UpdateBookAsync(Book book)
        {
            await _repository.UpdateBookAsync(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            await _repository.DeleteBookAsync(id);
        }

        public async Task RentBookAsync(string userId, int bookId, DateTime rentalDate)
        {
            var book = await _repository.GetBookByIdAsync(bookId);
            if (book == null)
            {
                throw new Exception("Kitap bulunamadı.");
            }

            var rental = new Rental
            {
                UserId = userId,
                BookId = bookId,
                RentalDate = rentalDate,
                TotalCost = 0
            };

            await _repository.AddRentalAsync(rental);
        }

        public async Task ReturnBookAsync(int rentalId)
        {

            var rental = await _repository.GetRentalsAsync();

            var rentals = rental.Where(r => r.RentalId == rentalId).FirstOrDefault();

            if (rental == null)
            {
                throw new Exception("Kiralama bulunamadı.");
            }

            rentals.ReturnDate = DateTime.Now;
            rentals.TotalCost = CalculateTotalCost(rentals.RentalDate, rentals.ReturnDate.Value, rentals.Book.DailyRentalRate);
            await _repository.UpdateRentalAsync(rentals);
        }

        private decimal CalculateTotalCost(DateTime rentalDate, DateTime returnDate, decimal dailyRate)
        {

            if (returnDate.Date == rentalDate.Date && returnDate.Hour < rentalDate.Hour)
            {
                return dailyRate;
            }

            int totalDays = (returnDate - rentalDate).Days;
            int businessDays = Enumerable.Range(0, totalDays + 1)
                                         .Select(d => rentalDate.AddDays(d))
                                         .Count(d => d.DayOfWeek != DayOfWeek.Saturday && d.DayOfWeek != DayOfWeek.Sunday);

            return businessDays * dailyRate;
        }

        public async Task<List<Rental>> GetRentalsByUserIdAsync(string userId)
        {
            return await _repository.GetRentalsByUserIdAsync(userId);
        }

        public async Task<List<Rental>> GetRentalsAsync()
        {
            return await _repository.GetRentalsAsync();
        }

    }
}
