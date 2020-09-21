using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;

namespace Store.DataAccess.Repositories.EFRepositories
{
    public class PrintingEditionRepository : BaseEFRepository<PrintingEdition>, IPrintingEditionRepository<PrintingEdition>
    {
        public PrintingEditionRepository(DbContextOptions<ApplicationContext> options) : base(options) { }
        public void Create(PrintingEdition item)
        {
            db.PrintingEditions.Add(item);
        }

        public void Delete(long id)
        {
            var pe = db.PrintingEditions.Find(id);
            if (pe != null) { db.PrintingEditions.Remove(pe); }
        }

        public PrintingEdition GetItem(long id)
        {
            return db.PrintingEditions.Find(id);
        }

        public IEnumerable<PrintingEdition> GetList()
        {
            return db.PrintingEditions;
        }
    }
}
