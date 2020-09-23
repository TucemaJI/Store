using System;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        void Update(T item);
        void Save();
    }
}
