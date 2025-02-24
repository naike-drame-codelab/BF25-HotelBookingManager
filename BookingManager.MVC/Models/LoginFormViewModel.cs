using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookingManager.MVC.Models
{
    public class LoginFormViewModel
    {
        [DisplayName("Username or Email")]
        [Required]
        public string UsernameOrEmail { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
