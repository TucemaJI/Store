using Dapper;
using Microsoft.Extensions.Options;
using Store.DataAccess.Entities;
using Store.DataAccess.Models.Filters;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using Store.Shared.Options;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DataAccess.Repositories.DapperRepositories
{
    public class OrderRepository : BaseDapperRepository<Order>, IOrderRepository
    {
        public OrderRepository(IOptions<ConnectionStringsOptions> options) : base(options) { }
        public async Task<(List<Order> orderList, long count)> GetOrderListAsync(OrderFilter filter)
        {
            using (var connection = CreateConnection())
            {
                var sql = new StringBuilder();
                sql.Append("SELECT O.*, U.Id, U.FirstName, U.LastName, U.Email, PE.Id, PE.Type, PE.Title, OI.* FROM Orders AS O ");
                sql.Append("JOIN OrderItems OI on OI.OrderId = O.Id JOIN AspNetUsers U on U.Id = O.UserId JOIN PrintingEditions PE on OI.PrintingEditionId = PE.Id ");
                sql.Append("WHERE (O.Status Like @status OR @status='0') AND (O.UserId = @id OR @id='')");
                var query = await connection.QueryAsync<Order, User, PrintingEdition, OrderItem, Order>(sql.ToString(),
                    (order, user, printingEdition, orderItem) =>
                    {
                        order.User = user;
                        orderItem.PrintingEdition = printingEdition;
                        order.OrderItems.Add(orderItem);

                        return order;
                    },
                    new { status = (int)filter.Status, id = filter.UserId is null ? string.Empty : filter.UserId },
                    splitOn: "Id");

                var groupedOrders = query.GroupBy(order => order.Id).Select(groupOrder =>
                {
                    var groupedOrder = groupOrder.First();
                    groupedOrder.OrderItems = groupOrder.Select(order => order.OrderItems.Single()).ToList();
                    return groupedOrder;
                });

                var orders = GetSortedList(filter, groupedOrders);
                var result = (orderList: orders, count: query.Count());
                return result;
            }
        }
        public override async Task<Order> GetItemAsync(long id)
        {
            using (var connection = CreateConnection())
            {
                var sql = new StringBuilder();
                sql.Append("SELECT O.*, U.Id, U.FirstName, U.LastName, U.Email, PE.Id, PE.Type, PE.Title, OI.* FROM Orders AS O ");
                sql.Append("JOIN OrderItems AS OI on OI.OrderId = O.Id JOIN AspNetUsers AS U on U.Id = O.UserId JOIN PrintingEditions AS PE on OI.PrintingEditionId = PE.Id ");
                sql.Append("WHERE O.Id = @orderId");

                var query = await connection.QueryAsync<Order, User, PrintingEdition, OrderItem, Order>(sql.ToString(),
                        (order, user, printingEdition, orderItem) =>
                        {
                            order.User = user;
                            orderItem.PrintingEdition = printingEdition;
                            order.OrderItems.Add(orderItem);

                            return order;
                        },
                        new { orderId = id },
                        splitOn: "Id");

                var groupedOrders = query.GroupBy(order => order.Id).Select(groupOrder =>
                {
                    var groupedOrder = groupOrder.First();
                    groupedOrder.OrderItems = groupOrder.Select(order => order.OrderItems.Single()).ToList();
                    return groupedOrder;
                });

                var order = groupedOrders.FirstOrDefault();

                return order;
            }
        }

    }
}
