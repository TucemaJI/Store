using AutoMapper;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository<Author> _authorRepository;
        private readonly IMapper _mapper;
        public AuthorService(IAuthorRepository<Author> authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task CreateAuthorAsync(AuthorModel model)
        {
            var author = _mapper.Map<AuthorModel, Author>(model);
            await _authorRepository.CreateAsync(author);
            await _authorRepository.SaveAsync();
        }

        public async Task<AuthorModel> GetAuthorModelAsync(long id)
        {
            var author = await _authorRepository.GetItemAsync(id);
            return _mapper.Map<Author, AuthorModel>(author);
        }

        public async Task<List<AuthorModel>> GetAuthorModelsAsync()
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

        public async Task DeleteAuthorAsync(long id)
        {
            await _authorRepository.DeleteAsync(id);
            await _authorRepository.SaveAsync();
        }

        public void UpdateAuthor(AuthorModel authorModel)
        {
            var author = _mapper.Map<AuthorModel, Author>(authorModel);
            _authorRepository.Update(author);
        }
    }
}
