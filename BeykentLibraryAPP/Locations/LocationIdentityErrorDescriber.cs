using Microsoft.AspNetCore.Identity;

namespace BeykentLibraryAPP.Locations
{
    public class LocationIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            return new()
            {
                Code = "DuplicateUserName",
                Description = $"{userName} daha başka bir kullanıcı tarafından alınmıştır."
            };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new()
            {
                Code = "DuplicateEmail",
                Description = $"Bu {email} hesabı ile zaten bir hesap oluşturulmuştur."
            };
        }


        public override IdentityError PasswordTooShort(int length)
        {
            return new()
            {
                Code = "PasswordTooShort",
                Description = "Şifre en az 6 karekterli olmalıdır."
            };
        }
    }
}
