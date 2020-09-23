using AutoMapper;
using Store.BusinessLogic.Models.Authors;
using Store.DataAccess.Entities;
using System.Linq;

namespace Store.BusinessLogic.Mappers
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<AuthorModel, Author>()
                .ForMember(a => a.Name, opt => opt.MapFrom(c => $"{c.FirstName} {c.LastName}"));
            CreateMap<Author, AuthorModel>()
                .ForMember(am => am.FirstName, opt => opt.MapFrom(c => GetFirstName(c)))
                .ForMember(am => am.LastName, opt => opt.MapFrom(c => GetSecondName(c)));
        }

        private string GetFirstName(Author author)
        {
            return GetNames(author).First();
        }
        private string GetSecondName(Author author)
        {
            return GetNames(author).Last();
        }
        private string[] GetNames(Author author)
        {
            return author.Name.Split(' ');
        }
    }
}
