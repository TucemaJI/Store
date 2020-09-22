using Store.BusinessLogic.Models.Authors;
using Store.DataAccess.Entities;

namespace Store.BusinessLogic.Mappers
{
    class AuthorMapper : BaseMapper<Author, AuthorModel>
    {
        public override Author Map(AuthorModel element)
        {
            return new Author
            {
                Name = element.Name
            };
        }

        public override AuthorModel Map(Author element)
        {
            return new AuthorModel
            {
                Name = element.Name
            };
        }
    }
}
