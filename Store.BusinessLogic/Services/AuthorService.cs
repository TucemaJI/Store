using AutoMapper;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public override void CreateEntityAsync(AuthorModel model)
        {
            var author = _mapper.Map<AuthorModel, Author>(model);
            _authorRepository.CreateAsync(author);
            _authorRepository.SaveAsync();
        }

        public async Task<AuthorModel> GetModelAsync(long id)
        {
            var author = await _authorRepository.GetItemAsync(id);
            return _mapper.Map<Author, AuthorModel>(author);
        }

        public override async Task<IEnumerable<AuthorModel>> GetModelsAsync()
        {
            var authorList = await _authorRepository.GetListAsync();
            var authorModelList = new List<AuthorModel>();
            foreach (var author in authorList)
            {
                var authorModel = _mapper.Map<Author, AuthorModel>(author);
                authorModelList.Add(authorModel);
            }
            return authorModelList;
        }
    }
}
