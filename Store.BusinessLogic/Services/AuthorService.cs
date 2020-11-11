using AutoMapper;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.DataAccess.Models;
using Store.DataAccess.Models.Filters;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task CreateAuthorAsync(AuthorModel model)
        {
            var author = _mapper.Map<AuthorModel, Author>(model);
            await _authorRepository.CreateAsync(author);
        }

        public async Task<AuthorModel> GetAuthorModelAsync(long id)
        {
            var author = await _authorRepository.GetItemAsync(id);
            return _mapper.Map<Author, AuthorModel>(author);
        }

        public async Task<List<AuthorModel>> GetAuthorModelsAsync(AuthorFilter filter)
        {
            var authorList = await _authorRepository.GetFilterSortedPagedListAsync(filter);
            return _mapper.Map<List<Author>, List<AuthorModel>>(authorList.ToList());
        }

        public async Task<List<AuthorModel>> GetAuthorModelsAsync()
        {
            var authorList = await _authorRepository.GetListAsync();
            return _mapper.Map<List<Author>, List<AuthorModel>>(authorList);
        }

        public async Task DeleteAuthorAsync(long id)
        {
            await _authorRepository.DeleteAsync(id);
        }

        public void UpdateAuthor(AuthorModel authorModel)
        {
            var author = _mapper.Map<AuthorModel, Author>(authorModel);
            _authorRepository.UpdateAsync(author);
        }
    }
}
