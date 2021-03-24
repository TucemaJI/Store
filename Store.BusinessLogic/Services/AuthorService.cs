using AutoMapper;
using Store.BusinessLogic.Exceptions;
using Store.BusinessLogic.Models;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.DataAccess.Models;
using Store.DataAccess.Models.Filters;
using Store.DataAccess.Repositories.Interfaces;
using Store.Shared.Constants;
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

        public Task CreateAuthorAsync(AuthorModel model)
        {
            var author = _mapper.Map<AuthorModel, Author>(model);
            var result = _authorRepository.CreateAsync(author);
            return result;
        }

        public async Task<AuthorModel> GetAuthorModelAsync(long id)
        {
            var author = _authorRepository.GetItemAsync(id);
            var result = _mapper.Map<AuthorModel>(await author);
            return result;
        }

        public async Task<PageModel<AuthorModel>> GetAuthorModelsAsync(AuthorFilter filter)
        {
            var authorQuery = _authorRepository.GetFilteredQuery(filter);
            var sortedAuthors = _authorRepository.GetSortedListAsync(filter: filter, query: authorQuery);
            var authorModelList = _mapper.Map<List<Author>, List<AuthorModel>>(await sortedAuthors);
            var pagedList = new PagedList<AuthorModel>(authorModelList, authorQuery.Count(), pageNumber: filter.EntityParameters.CurrentPage,
                pageSize: filter.EntityParameters.ItemsPerPage);

            var pageModel = new PageModel<AuthorModel>(pagedList);
            return pageModel;
        }

        public Task DeleteAuthorAsync(AuthorModel authorModel)
        {
            return _authorRepository.DeleteAsync(authorModel.Id);
        }

        public async Task UpdateAuthorAsync(AuthorModel authorModel)
        {
            var author = await _authorRepository.GetItemAsync(authorModel.Id);
            if (author is null)
            {
                throw new BusinessLogicException(ExceptionOptions.AUTHOR_NOT_FOUND);
            }
            var mappedEntity = _mapper.Map<Author>(authorModel);
            await _authorRepository.UpdateAsync(mappedEntity);
        }
    }
}
