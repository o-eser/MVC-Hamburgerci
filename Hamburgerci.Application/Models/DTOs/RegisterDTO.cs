using Hamburgerci.Entities.Enum;

namespace Hamburgerci.Application.Models.DTOs
{
    public class RegisterDTO
    {
        //Todo: DataAnnotations
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate => DateTime.Now;
        public DataStatus DataStatus => DataStatus.Inserted;
    }
}
