﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManager.DAL.Repositories
{
    // abstract : cette class ne sert que pour l'héritage
    public abstract class CrudRepositoryBase<T> where T : class
    {
        public virtual List<T> GetAll()
        {
            using HotelContext ctx = new HotelContext();
            return ctx.Set<T>().ToList();
        }

        public virtual T? GetById(int id)
        {
            using HotelContext ctx = new HotelContext();
            return ctx.Set<T>().Find(id);
        }

        public virtual void Remove(T entity)
        {
            using HotelContext ctx = new HotelContext();
            ctx.Set<T>().Remove(entity);
            ctx.SaveChanges();
        }

        public virtual T Add(T entity)
        {
            using HotelContext ctx = new HotelContext();
            T result = ctx.Set<T>().Add(entity).Entity;
            ctx.SaveChanges();
            return result;
        }

        public virtual T Update(T entity)
        {
            using HotelContext ctx = new HotelContext();
            T result = ctx.Set<T>().Update(entity).Entity;
            ctx.SaveChanges();
            return result;
        }


    }
}
