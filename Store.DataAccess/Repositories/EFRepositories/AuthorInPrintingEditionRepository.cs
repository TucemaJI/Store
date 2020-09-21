using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;

namespace Store.DataAccess.Repositories.EFRepositories
{
    public class AuthorInPrintingEditionRepository :BaseEFRepository<AuthorInPrintingEdition>, IAuthorInPrintingEditionRepository<AuthorInPrintingEdition>
    {
        public AuthorInPrintingEditionRepository(DbContextOptions<ApplicationContext> options) :base(options){}
        public void Create(AuthorInPrintingEdition item)
        {
            db.AuthorsInPrintingEditions.Add(item);
        }

        public void Delete(long id)
        {
            var aipe = db.AuthorsInPrintingEditions.Find(id);
            if (aipe != null) { db.AuthorsInPrintingEditions.Remove(aipe); }
        }

        public AuthorInPrintingEdition GetItem(long id)
        {
            return db.AuthorsInPrintingEditions.Find(id);
        }

        public IEnumerable<AuthorInPrintingEdition> GetList()
        {
            return db.AuthorsInPrintingEditions;
        }

    }
}
