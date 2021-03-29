﻿using AutoMapper;
using Store.BusinessLogic.Exceptions;
using Store.BusinessLogic.Models;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Entities;
using Store.DataAccess.Models.Filters;
using Store.DataAccess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Store.Shared.Constants.Constants;

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
            var author = _mapper.Map<Author>(model);
            var result = _authorRepository.CreateAsync(author);
            return result;
        }

        public async Task<AuthorModel> GetAuthorModelAsync(long id)
        {
            var author = await _authorRepository.GetItemAsync(id);
            if (author is null)
            {
                throw new BusinessLogicException(ExceptionConsts.AUTHOR_NOT_FOUND);
            }
            var result = _mapper.Map<AuthorModel>(author);
            return result;
        }

        public async Task<PageModel<AuthorModel>> GetAuthorModelListAsync(AuthorFilter filter)
        {
            var sortedAuthors = await _authorRepository.GetAuthorListAsync(filter);
            var authorModelList = _mapper.Map<List<AuthorModel>>(sortedAuthors);
            var pageModel = new PageModel<AuthorModel>(authorModelList, filter.PageOptions);
            return pageModel;
        }

        public async Task DeleteAuthorAsync(long id)
        {
            var author = await _authorRepository.GetItemAsync(id);
            if(author is null)
            {
                throw new BusinessLogicException(ExceptionConsts.AUTHOR_NOT_FOUND);
            }
            await _authorRepository.DeleteAsync(author);
        }

        public async Task UpdateAuthorAsync(AuthorModel authorModel)
        {
            var author = await _authorRepository.GetItemAsync(authorModel.Id);
            if (author is null)
            {
                throw new BusinessLogicException(ExceptionConsts.AUTHOR_NOT_FOUND);
            }
            var mappedEntity = _mapper.Map<Author>(authorModel);
            await _authorRepository.UpdateAsync(mappedEntity);
        }
    }
}
