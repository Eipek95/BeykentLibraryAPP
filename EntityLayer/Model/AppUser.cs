using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Model
{
    public class AppUser : IdentityUser
    {
        public ICollection<Rental> Rentals { get; set; }
    }
}
