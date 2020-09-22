using Store.BusinessLogic.Mappers;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;

namespace Store.BusinessLogic.Services
{
    public class AuthorService : BaseService<AuthorModel>, IAuthorService
    {
        private readonly IAuthorRepository<Author> _authorRepository;

        public AuthorService(IAuthorRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public override void Create(AuthorModel item)
        {
            _authorRepository.Create(new AuthorMapper().Map(item));
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

        public override void Save()
        {
            throw new System.NotImplementedException();
        }

        public override void Update(AuthorModel item)
        {
            throw new System.NotImplementedException();
        }
    }
}
