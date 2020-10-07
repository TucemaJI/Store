using Store.BusinessLogic.Models.Authors;
using Store.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IAuthorService 
    {
        public Task<AuthorModel> GetAuthorModelAsync(long id);
        public Task CreateAuthorAsync(AuthorModel model);

        public Task<List<AuthorModel>> GetAuthorModelsAsync();
        public Task<PagedList<AuthorModel>> GetAuthorModelsAsync(EntityParameters entityParameters);
        public Task DeleteAuthorAsync(long id);
        public void UpdateAuthor(AuthorModel authorModel);
    }
}
