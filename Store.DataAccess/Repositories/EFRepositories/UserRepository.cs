using Microsoft.EntityFrameworkCore;
using Store.DataAccess.AppContext;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;

namespace Store.DataAccess.Repositories.EFRepositories
{
    public class UserRepository : BaseEFRepository<User>, IUserRepository<User>
    {
        public UserRepository(DbContextOptions<ApplicationContext> options) : base(options) { }
        public void Create(User item)
        {
            db.Users.Add(item);
            db.SaveChanges();
        }

        public void Delete(long id)
        {
            User user = (User)db.Users.Find(id);
            if (user != null) { db.Users.Remove(user); }

        }

        public User GetItem(long id)
        {
            return (User)db.Users.Find(id);
        }

        public IEnumerable<User> GetList()
        {
            return (IEnumerable<User>)db.Users;
        }
    }
}
