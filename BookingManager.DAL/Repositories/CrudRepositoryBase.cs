using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManager.DAL.Repositories
{
    // abstract : cette class ne sert que pour l'héritage
    public abstract class CrudRepositoryBase<T>(HotelContext ctx) where T : class
    {
        public virtual List<T> GetAll()
        {
            return ctx.Set<T>().ToList();
        }

        public virtual T? GetById(int id)
        {
            return ctx.Set<T>().Find(id);
        }

        public virtual void Remove(T entity)
        {
            ctx.Set<T>().Remove(entity);
            ctx.SaveChanges();
        }

        public virtual T Add(T entity)
        {
            T result = ctx.Set<T>().Add(entity).Entity;
            ctx.SaveChanges();
            return result;
        }

        public virtual T Update(T entity)
        {
            T result = ctx.Set<T>().Update(entity).Entity;
            ctx.SaveChanges();
            return result;
        }
    }
}
