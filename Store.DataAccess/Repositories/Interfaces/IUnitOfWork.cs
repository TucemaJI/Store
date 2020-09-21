using Store.DataAccess.Entities;

namespace Store.DataAccess.Repositories.Interfaces
{
    interface IUnitOfWork
    {
        IAuthorInPrintingEditionRepository<AuthorInPrintingEdition> AuthorInPrintingEdition { get; }
        IAuthorRepository<Author> Author { get; }
        IOrderItemRepository<OrderItem> OrderItem { get; }
        IOrderRepository<Order> Order { get; }
        IPrintingEditionRepository<PrintingEdition> PrintingEdition { get; }
        IUserRepository<User> User { get; }
        void Save();
    }
}
