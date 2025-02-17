using System.ComponentModel.DataAnnotations;

namespace BookingManager.MVC.Models
{
    public class ContactFormViewModel
    {
        //[Required(ErrorMessage = "Ce champ est requis.")]
        //[RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Cet email n'est pas valide.")]
        //public string Email { get; set; } = null!;
        
        [Required(ErrorMessage = "Ce champ est requis.")]
        [MaxLength(50, ErrorMessage = "Ce message est trop long.")]
        public string Subject { get; set; } = null!;

        [Required(ErrorMessage = "Ce champ est requis.")]
        [MinLength(10, ErrorMessage = "C'est trop court.")]
        public string Message { get; set; } = null!;
    }
}
