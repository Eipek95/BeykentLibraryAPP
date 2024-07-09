using System.ComponentModel.DataAnnotations;

namespace BeykentLibraryAPP.Models
{
    public class PasswordChangeVewModel
    {
        public string PasswordOld { get; set; } = null!;
        public string PasswordNew { get; set; } = null!;
        [Compare(nameof(PasswordNew), ErrorMessage = "Şifreler Aynı Değil")]
        public string PasswordNewConfirm { get; set; } = null!;
    }
}
