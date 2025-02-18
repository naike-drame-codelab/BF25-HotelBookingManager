using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookingManager.MVC.Models
{
    public class CustomerCreateFormViewModel
    {
        [DisplayName("Nom")]
        [Required(ErrorMessage = "Champ obligatoire.")]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; } = null!;

        [DisplayName("Prénom")]
        [Required(ErrorMessage = "Champ obligatoire.")]
        [StringLength(50, MinimumLength = 2)]

        public string FirstName { get; set; } = null!;

        [DisplayName("Email")]
        [Required(ErrorMessage = "Champ obligatoire.")]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [DisplayName("N° Téléphone")]
        [StringLength(50)]
        public string? PhoneNumber { get; set; } = null!;
    }
}
