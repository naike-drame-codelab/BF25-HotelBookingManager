﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManager.DAL.Entities
{
    [Table("Option")]
    public class Option
    {
        public int OptionId { get; set; }

        public string Name { get; set; } = null!;

        public List<Room> Rooms { get; set; } = null!;

    }
}
