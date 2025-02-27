﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookingManager.DAL.Entities
{
    [Table("Room")]
    [Index(nameof(ImageUrl), IsUnique = true)]
    public class Room
    {
        public int RoomId { get; set; }
        public string Number { get; set; } = null!;
        public int Floor { get; set; }
        public int Surface { get; set; }
        public string ImageUrl { get; set; } = null!;
        public int MaxCapacity { get; set; }
        public decimal Price { get; set; }
        public List<Option> Options { get; set; } = null!;
        public List<Booking> Bookings { get; set; } = null!;
    }
}
