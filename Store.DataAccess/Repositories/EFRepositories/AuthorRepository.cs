using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;

namespace Store.DataAccess.Repositories.EFRepositories
{
    public class AuthorRepository : BaseEFRepository<Author>, IAuthorRepository<Author>
    {
        public AuthorRepository(DbContextOptions<ApplicationContext> options) : base(options) { }
        public void Create(Author item)
        {
            db.Authors.Add(item);
        }

        public void Delete(long item)
        {
            var author = db.Authors.Find(item);
            if (author != null) { db.Authors.Remove(author); }
        }

        public Author GetItem(long id)
        {
            return db.Authors.Find(id);
        }

        public IEnumerable<Author> GetList()
        {
            return db.Authors;
        }

    }
}
