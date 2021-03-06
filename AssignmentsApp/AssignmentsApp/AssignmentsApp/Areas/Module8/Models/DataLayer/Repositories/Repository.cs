﻿using AssignmentsApp.Areas.Module8.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentsApp.Areas.Module8.Models.DataLayer.Repositories
{
    public class Repository<T> : IRepository<T> where T: class
    {
        protected Module8Context context { get; set; }
        private DbSet<T> dbset { get; set; }

        private int? count;

        public int Count => count ?? dbset.Count();

        public Repository(Module8Context ctx)
        {
            context = ctx;
            dbset = context.Set<T>();
        }

        public virtual IEnumerable<T> List(QueryOptions<T> options)
        {
            IQueryable<T> query = dbset;
            foreach (string include in options.GetIncludes())
            {
                query = query.Include(include);
            }
            if (options.HasWhere)
                query = query.Where(options.Where);
                count = query.Count();
            if (options.HasOrderBy)
                query = query.OrderBy(options.OrderBy);
            if (options.HasPaging)
                query = query.PageBy(options.PageNumber, options.PageSize);
            return query.ToList();
        }

        public virtual T Get(int id) => dbset.Find(id);
        public virtual void Insert(T entity) => dbset.Add(entity);
        public virtual void Update(T entity) => dbset.Update(entity);
        public virtual void Delete(T entity) => dbset.Remove(entity);
        public virtual void Save() => context.SaveChanges();
    }
}
