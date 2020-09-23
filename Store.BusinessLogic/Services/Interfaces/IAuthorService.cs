using Store.BusinessLogic.Models.Authors;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IAuthorService 
    {
        public abstract AuthorModel GetAuthor(long id);
    }
}
