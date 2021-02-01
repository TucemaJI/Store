﻿using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Models;
using Store.DataAccess.Models.Filters;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.EFRepositories
{
    public class AuthorRepository : BaseEFRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationContext applicationContext) : base(applicationContext) { }

        public Task<PagedList<Author>> GetFilterSortedPagedListAsync(AuthorFilter filter)
        {
            var authors = _dbSet.Where(a => EF.Functions.Like(a.Name, $"%{filter.Name}%"));
            var sortedAuthors = GetSortedPagedList(filter: filter, ts: authors);
            return sortedAuthors;
        }
    }
}
