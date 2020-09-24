using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;

namespace Store.DataAccess.Repositories.Base
{
    public abstract class BaseEFRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationContext db;
        public BaseEFRepository(DbContextOptions<ApplicationContext> options)
        {
            db = new ApplicationContext(options);
        }

        public void Create(T entity)
        {
            db.Set<T>().Add(entity: entity);
        }
        public void Delete(long item)
        {
            db.Set<T>().Remove(db.Set<T>().Find(item));
        }
        public T GetItem(long id)
        {
            return db.Set<T>().Find(id);
        }
        public IEnumerable<T> GetList()
        {
            return db.Set<T>();
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
