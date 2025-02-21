using System.ComponentModel;

namespace BookingManager.MVC.Models
{
    public record CustomerDetailsViewModel
    (
        int Id,
        string LastName,
        string FirstName,
        string Email,
        string PhoneNumber,
        int BookingsNb
    )
    {
        // définit le label qui va être affiché au niveau de l'html
        [DisplayName("ID")]
        public int Id { get; } = Id;
        [DisplayName("Nom")]
        public string LastName { get; } = LastName;
        [DisplayName("Prénom")]
        public string FirstName { get; } = FirstName;
        [DisplayName("N° Téléphone")]
        public string? PhoneNumber { get; } = PhoneNumber;
        [DisplayName("Email")]
        public string Email { get; } = Email;
        [DisplayName("Nombre de réservations")]
        public int BookingsNb { get; } = BookingsNb;

    }
}

