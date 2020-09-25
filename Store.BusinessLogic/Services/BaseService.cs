using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services
{
    public abstract class BaseService<T> where T : class
    {
        public abstract void CreateEntityAsync(T model);
        
        public abstract Task<IEnumerable<T>> GetModelsAsync();
    }
}
