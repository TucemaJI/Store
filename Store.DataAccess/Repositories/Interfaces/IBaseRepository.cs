using System;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IBaseRepository<T> : IDisposable where T : class
    {
        void Update(T item);
        void Save();
    }
}
