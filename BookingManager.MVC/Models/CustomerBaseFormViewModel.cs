using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BookingManager.MVC.Models
{
    public class CustomerBaseFormViewModel
    {
        [DisplayName("Nom")]
        [Required(ErrorMessage = "Champ obligatoire.")]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; } = null!;

        [DisplayName("Prénom")]
        [Required(ErrorMessage = "Champ obligatoire.")]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; } = null!;

        [DisplayName("N° Téléphone")]
        [StringLength(50)]
        public string? PhoneNumber { get; set; } = null!;
    }
}
