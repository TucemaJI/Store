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

    }
}
