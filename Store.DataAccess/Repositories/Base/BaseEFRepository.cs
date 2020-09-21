using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Repositories.Interfaces;
using System;

namespace Store.DataAccess.Repositories.Base
{
    public abstract class BaseEFRepository<T> : IBaseRepository<T> where T : class
    {
        private bool disposed = false;
        protected ApplicationContext db;
        public BaseEFRepository(DbContextOptions<ApplicationContext> options)
        {
            db = new ApplicationContext(options);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing && !disposed)
            {
                db.Dispose();
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            db.SaveChanges();
        }
        public void Update(T item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
