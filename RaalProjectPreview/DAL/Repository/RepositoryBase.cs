using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using RaalProjectPreview.DAL.Models;
using RaalProjectPreview.DAL.Repository.Interface;
namespace RaalProjectPreview.DAL.Repository
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _APPContext;
        protected readonly IDbSet<T> _DbSet;
        public RepositoryBase(DbContext context)
        {
            _APPContext = context;
            _DbSet = _APPContext.Set<T>();
        }
        public T Create(T item)
        {
            T newItem = _DbSet.Add(item);
            _APPContext.SaveChanges();
            return newItem;
        }

        public T Delete(T item)
        {
            T removedItem =_DbSet.Remove(item);
            _APPContext.SaveChanges();
            return removedItem;
        }

        public List<T> GetAll()
        {
            return _DbSet.ToList();
        }

        public List<T> GetList(Func<T, bool> query)
        {
            return _DbSet.Where(query).ToList();
        }
    }
}