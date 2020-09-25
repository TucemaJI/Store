using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;

namespace Store.DataAccess.Repositories.EFRepositories
{
    public class AuthorInPrintingEditionRepository :BaseEFRepository<AuthorInPrintingEdition>, IAuthorInPrintingEditionRepository<AuthorInPrintingEdition>
    {
        public AuthorInPrintingEditionRepository(ApplicationContext db) :base(db){}

    }
}
