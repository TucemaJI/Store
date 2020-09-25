using Store.BusinessLogic.Models.Authors;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IAuthorService 
    {
        public abstract Task<AuthorModel> GetModelAsync(long id);
    }
}
