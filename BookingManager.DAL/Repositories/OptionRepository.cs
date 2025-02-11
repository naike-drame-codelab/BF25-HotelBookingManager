using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManager.Application.Abstractions;
using BookingManager.DAL.Entities;

namespace BookingManager.DAL.Repositories
{
    public class OptionRepository : CrudRepositoryBase<Option>, IOptionRepository
    {
      
    }
}
