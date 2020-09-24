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
                .ForMember(am => am.FirstName, opt => opt.MapFrom(c => GetNames(c).FirstOrDefault()))
                .ForMember(am => am.LastName, opt => opt.MapFrom(c => GetNames(c).LastOrDefault()));
        }

        private string[] GetNames(Author author)
        {
            return author.Name.Split(' ');
        }
    }
}
