using Store.BusinessLogic.Models.Authors;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IAuthorService 
    {
        public abstract AuthorModel GetAuthor(long id);
    }
}
