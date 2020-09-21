using System;

namespace Store.DataAccess.Repositories.Interfaces
{
    interface IBaseRepository<T> : IDisposable where T : class
    {
        void Update(T item);
        void Save();
    }
}
