using Store.DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.Interfaces
{
    public interface IAuthorInPrintingEditionRepository : IBaseRepository<AuthorInPrintingEdition>
    {
        public Task CreateRangeAsync(List<AuthorInPrintingEdition> authorInPrintingEditionList);
    }
}
