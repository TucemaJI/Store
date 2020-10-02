using Store.BusinessLogic.Models.Authors;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IAuthorService 
    {
        public Task<AuthorModel> GetAuthorModelAsync(long id);
        public Task CreateAuthorAsync(AuthorModel model);

        public Task<IEnumerable<AuthorModel>> GetAuthorModelsAsync();
    }
}
