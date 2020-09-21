﻿using Store.DataAccess.Repositories.Base;
using Store.DataAccess.Repositories.EFRepositories;
using System.Collections.Generic;

namespace Store.BusinessLogic.Services
{
    public abstract class BaseService<T> where T : class
    {
        public EFUnitOfWork Db { get; set; }
        public abstract void Update(T item);
        public virtual void Save()
        {
            Db.Save();
        }
        public abstract IEnumerable<T> GetList();
        public abstract T GetItem(long id);
        public abstract void Create(T item);
        public abstract void Delete(long id);
    }
}
