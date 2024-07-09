using EntityLayer.Model;
using Microsoft.AspNetCore.Identity;

namespace BeykentLibraryAPP.CustomValidations
{
    public class PasswordValidator : IPasswordValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string? password)
        {
            var errors = new List<IdentityError>();
            if (password!.ToLower().Contains(user.UserName!.ToLower()))
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordContainUserName",
                    Description = "Şifre Alanı Kullanıcı Adı İçeremez",
                });
            }

            if (password!.ToLower().StartsWith("1234"))
            {
                errors.Add(new()
                {
                    Code = "PasswordContain1234",
                    Description = "Şifre Alanı Ardışık Sayı İçeremez"
                });
            }

            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}
