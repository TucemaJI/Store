using Store.BusinessLogic.Models;
using Store.BusinessLogic.Models.Authors;
using Store.DataAccess.Models.Filters;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IAuthorService
    {
        public Task<AuthorModel> GetAuthorModelAsync(long id);
        public Task CreateAuthorAsync(AuthorModel model);
        public Task<PageModel<AuthorModel>> GetAuthorModelListAsync(AuthorFilter filter);
        public Task DeleteAuthorAsync(long id);
        public Task UpdateAuthorAsync(AuthorModel authorModel);
    }
}
