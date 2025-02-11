using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookingManager.DAL.Entities
{
    [Table("Login")]
    [Index(nameof(Username), IsUnique = true)] // accélère la recherche dans la db et spécifier qu'il doit être unique
    public class Login
    {
        public int LoginId { get; set; }
        public string Username { get; set; } = null!;
        public byte[] Password { get; set; } = null!;
        public virtual string Role { get; } = "Admin";
    }
}
