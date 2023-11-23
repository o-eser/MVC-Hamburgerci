using System.ComponentModel.DataAnnotations;

namespace Hamburgerci.UI.Models.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Kullanıcı Adı Boş Geçilemez!")]
        [Display(Name = "Kullanıcı Adı")]
        [MinLength(3, ErrorMessage = "Kullanıcı Adı 3 Karakterden Az Olamaz!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre boş geçilemez!")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email boş geçilemez!")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Mail Formatı Doğrulanamadı!")]
        public string Email { get; set; }
    }
}
