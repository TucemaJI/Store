using System.Collections.Generic;

namespace Store.BusinessLogic.Services
{
    public abstract class BaseService<T> where T : class
    {
        public abstract void CreateEntity(T model);
        
        public abstract IEnumerable<T> GetModels();
    }
}
