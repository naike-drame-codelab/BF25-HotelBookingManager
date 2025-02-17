
namespace BookingManager.MVC.Models
{
    // record crée une class immuable dont on ne peut plus modifier les éléments
    public record CustomerIndexViewModel
    (
        int Id,
        string LastName,
        string FirstName,
        string Email,
        int BookingsNb
    );
}
