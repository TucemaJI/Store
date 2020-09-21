using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using System.Collections.Generic;

namespace Store.BusinessLogic.Services
{
    public class AuthorService : BaseService<AuthorModel>, IAuthorService
    {
        public override void Create(AuthorModel item)
        {
            //Db.Author.Create((Author)item);
        }

        public override void Delete(long id)
        {
            throw new System.NotImplementedException();
        }

        public override AuthorModel GetItem(long id)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<AuthorModel> GetList()
        {
            throw new System.NotImplementedException();
        }

        public override void Update(AuthorModel item)
        {
            throw new System.NotImplementedException();
        }
    }
}
