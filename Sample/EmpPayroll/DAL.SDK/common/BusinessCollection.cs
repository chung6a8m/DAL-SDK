﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.SDK.common
{
    public class BusinessCollection<TModel, TContext>
        where TModel : class, new()
        where TContext : DbContext, new()

    {
        private TContext _contx = null;
        DbSet<TModel> dbSet;

        public TContext ContextObject
        {
            get { return _contx ?? (_contx = new TContext()); }
        }

        public BusinessCollection()
        {
            this.dbSet = ContextObject.Set<TModel>();

        }

        public virtual IEnumerable<TModel> Get(
           Expression<Func<TModel, bool>> filter = null,
           Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null,
           string includeProperties = "")
        {
            IQueryable<TModel> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TModel GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public TModel Insert(TModel entity)
        {
            return dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TModel entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TModel entityToDelete)
        {
            if (ContextObject.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TModel entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            ContextObject.Entry(entityToUpdate).State = EntityState.Modified;
        }


    }
}
