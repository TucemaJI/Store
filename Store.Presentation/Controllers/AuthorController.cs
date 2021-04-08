using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogic.Models;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Models.Filters;
using Store.Presentation.Controllers.Base;
using System.Threading.Tasks;
using static Store.Shared.Enums.Enums;

namespace Store.Presentation.Controllers
{
    public class AuthorController : BaseController
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost("GetAuthorsWithFilter")]
        public Task<PageModel<AuthorModel>> GetAuthorModelsAsync([FromBody] AuthorFilter filter)
        {
            var result = _authorService.GetAuthorModelListAsync(filter);
            return result;
        }

        [HttpGet("{id:long}")]
        public Task<AuthorModel> GetAuthorModelAsync(long id)
        {
            var result = _authorService.GetAuthorModelAsync(id);
            return result;
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost("CreateAuthor")]
        public async Task CreateAuthorAsync([FromBody] AuthorModel authorModel)
        {
            await _authorService.CreateAuthorAsync(authorModel);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost("DeleteAuthor")]

        public async Task DeleteAuthorAsync([FromBody] AuthorModel authorModel)
        {
            await _authorService.DeleteAuthorAsync(authorModel.Id);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost("UpdateAuthor")]
        public async Task UpdateAuthor([FromBody] AuthorModel authorModel)
        {
            await _authorService.UpdateAuthorAsync(authorModel);
        }
    }
}
