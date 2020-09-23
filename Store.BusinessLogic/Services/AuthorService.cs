using AutoMapper;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;

namespace Store.BusinessLogic.Services
{
    public class AuthorService : BaseService<AuthorModel>, IAuthorService
    {
        private readonly IAuthorRepository<Author> _authorRepository;
        private readonly IMapper _mapper;
        public AuthorService(IAuthorRepository<Author> authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public override void CreateEntity(AuthorModel model)
        {
            _authorRepository.Create(_mapper.Map<AuthorModel, Author>(model));
            _authorRepository.Save();
        }

        public AuthorModel GetAuthor(long id)
        {
            return _mapper.Map<Author, AuthorModel>(_authorRepository.GetItem(id));
        }

        public override IEnumerable<AuthorModel> GetModels()
        {
            var authorList = _authorRepository.GetList();
            var authorModelList = new List<AuthorModel>();
            foreach (var author in authorList)
            {
                authorModelList.Add(_mapper.Map<Author, AuthorModel>(author));
            }
            return authorModelList;
        }
    }
}
