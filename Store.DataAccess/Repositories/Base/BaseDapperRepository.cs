using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Store.DataAccess.Models.Filters;
using Store.DataAccess.Repositories.Interfaces;
using Store.Shared.Options;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Store.DataAccess.Extentions;
using static Store.Shared.Constants.Constants;

namespace Store.DataAccess.Repositories.Base
{
    public class BaseDapperRepository<T> : IDapperRepository<T> where T : class
    {
        private string _connectionString;
        public BaseDapperRepository(IOptions<ConnectionStringsOptions> options)
        {
            _connectionString = options.Value.DefaultConnection;
        }

        private SqlConnection SqlConnection()
        {
            var sqlConnection = new SqlConnection(_connectionString);
            return sqlConnection;
        }

        protected IDbConnection CreateConnection()
        {
            var connection = SqlConnection();
            connection.Open();
            return connection;
        }
        public async Task CreateAsync(T item)
        {
            using (var connection = CreateConnection())
            {
                await connection.InsertAsync(item);
            }
        }

        public async Task DeleteAsync(T item)
        {
            using (var connection = CreateConnection())
            {
                await connection.DeleteAsync(item);
            }
        }

        public async Task<T> GetItemAsync(long id)
        {
            using (var connection = CreateConnection())
            {
                var result = await connection.GetAsync<T>(id);
                return result;
            }
        }

        public List<T> GetSortedList(BaseFilter filter, IEnumerable<T> query)
        {
            if (string.IsNullOrWhiteSpace(filter.OrderByString))
            {
                filter.OrderByString = RepositoryConsts.DEFAULT_SEARCH;
            }
            var sortedT = query.AsQueryable().OrderBy(filter.OrderByString, filter.IsDescending)
                .ToSortedList(filter.PageOptions.CurrentPage, filter.PageOptions.ItemsPerPage);

            return sortedT;
        }

        public async Task UpdateAsync(T item)
        {
            using (var connection = CreateConnection())
            {
                await connection.UpdateAsync(item);
            }
        }

    }
}
