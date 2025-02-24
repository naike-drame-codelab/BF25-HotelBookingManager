using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingManager.Application.Abstractions.Repositories;
using BookingManager.DAL.Entities;

namespace BookingManager.DAL.Repositories
{
    public class LoginRepository(HotelContext ctx) : CrudRepositoryBase<Login>(ctx), ILoginRepository
    {
        public Login? GetByUsername(string username)
        {
            return ctx.Logins.SingleOrDefault(l => l.Username == username);
        }
    }
}
