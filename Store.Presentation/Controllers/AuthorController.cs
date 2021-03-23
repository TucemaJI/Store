using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.BusinessLogic.Models;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAccess.Models.Filters;
using Store.Presentation.Controllers.Base;
using System.Threading.Tasks;
using static Store.Shared.Enums.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Presentation.Controllers
{
    public class AuthorController : BaseController
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService, ILogger<AuthorController> logger) : base(logger)
        {
            _authorService = authorService;
        }

        [HttpPost("GetAuthorsWithFilter")]
        public Task<PageModel<AuthorModel>> GetAuthorModelsAsync([FromBody] AuthorFilter filter)
        {
            return _authorService.GetAuthorModelsAsync(filter);
        }

        [HttpGet("{id:long}")]
        public async Task<AuthorModel> GetAuthorModelAsync(long id)
        {
            return await _authorService.GetAuthorModelAsync(id);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost("CreateAuthor")]
        public Task CreateAuthorAsync([FromBody] AuthorModel authorModel)
        {
            return _authorService.CreateAuthorAsync(authorModel);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost("DeleteAuthor")]

        public async Task DeleteAuthorAsync([FromBody] AuthorModel authorModel)
        {
            await _authorService.DeleteAuthorAsync(authorModel);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost("UpdateAuthor")]
        public void UpdateAuthor([FromBody] AuthorModel authorModel)
        {
            _authorService.UpdateAuthorAsync(authorModel);
        }
    }
}
