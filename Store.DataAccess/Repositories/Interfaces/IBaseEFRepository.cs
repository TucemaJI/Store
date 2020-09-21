using System.Collections.Generic;

namespace Store.DataAccess.Repositories.Interfaces
{
    interface IBaseEFRepository<T> : IBaseRepository<T> where T : class
    {
        IEnumerable<T> GetList();
        T GetItem(long id);
        void Create(T item);
        void Delete(long id);
    }
}
