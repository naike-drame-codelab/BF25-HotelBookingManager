using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManager.DAL.Entities;

namespace BookingManager.Application.Abstractions
{
    public interface IOptionRepository : ICrudRepository<Option>
    {
    }
}
