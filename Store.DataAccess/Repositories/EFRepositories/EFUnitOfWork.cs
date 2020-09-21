using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using System;

namespace Store.DataAccess.Repositories.EFRepositories
{
    public class EFUnitOfWork : IUnitOfWork, IDisposable
    {
        private bool disposed = false;
        private ApplicationContext db;
        private DbContextOptions<ApplicationContext> options;
        private AuthorInPrintingEditionRepository authorInPrintingEditionRepository;
        private AuthorRepository authorRepository;
        private OrderItemRepository orderItemRepository;
        private OrderRepository orderRepository;
        private PrintingEditionRepository printingEditionRepository;
        private UserRepository userRepository;

        public EFUnitOfWork(DbContextOptions<ApplicationContext> options)
        {
            this.options = options;
            db = new ApplicationContext(options);
        }

        public IAuthorInPrintingEditionRepository<AuthorInPrintingEdition> AuthorInPrintingEdition
        {
            get
            {
                if (authorInPrintingEditionRepository == null) { authorInPrintingEditionRepository = new AuthorInPrintingEditionRepository(options); }
                return authorInPrintingEditionRepository;
            }
        }

        public IAuthorRepository<Author> Author
        {
            get
            {
                if (authorRepository == null) { authorRepository = new AuthorRepository(options); }
                return authorRepository;
            }
        }

        public IOrderItemRepository<OrderItem> OrderItem
        {
            get
            {
                if (orderItemRepository == null) { orderItemRepository = new OrderItemRepository(options); }
                return orderItemRepository;
            }
        }

        public IOrderRepository<Order> Order
        {
            get
            {
                if (orderRepository == null) { orderRepository = new OrderRepository(options); }
                return orderRepository;
            }
        }

        public IPrintingEditionRepository<PrintingEdition> PrintingEdition
        {
            get
            {
                if (printingEditionRepository == null) { printingEditionRepository = new PrintingEditionRepository(options); }
                return printingEditionRepository;
            }
        }

        public IUserRepository<User> User
        {
            get
            {
                if (userRepository == null) { userRepository = new UserRepository(options); }
                return userRepository;
            }
        }



        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
