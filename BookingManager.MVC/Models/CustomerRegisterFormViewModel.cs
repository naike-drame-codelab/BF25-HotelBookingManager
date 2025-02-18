using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookingManager.MVC.Models
{
    public class CustomerRegisterFormViewModel
    {
        [DisplayName("Nom")]
        [Required(ErrorMessage = "Ce champ est requis.")]
        [MinLength(2, ErrorMessage = "Le nom requiert au minimum 2 caractères.")]
        [MaxLength(50, ErrorMessage = "Ce message est trop long.")]
        public string LastName { get; set; } = null!;

        [DisplayName("Prénom")]
        [Required(ErrorMessage = "Ce champ est requis.")]
        [MinLength(2, ErrorMessage = "Le nom requiert au minimum 2 caractères.")]
        [MaxLength(50, ErrorMessage = "Ce message est trop long.")]
        public string FirstName { get; set; } = null!;

        [DisplayName("Email")]
        [Required(ErrorMessage = "Ce champ est requis.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Cet email n'est pas valide.")]
        public string Email { get; set; } = null!;

        [DisplayName("Numéro de téléphone")]
        [Required(ErrorMessage = "Ce champ est requis.")]
        [MaxLength(50, ErrorMessage = "Ce numéro de téléphone est trop long.")]
        public string? PhoneNumber { get; set; }
    }
}
