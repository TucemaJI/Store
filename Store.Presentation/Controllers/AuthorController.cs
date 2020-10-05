using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.BusinessLogic.Models.Authors;
using Store.BusinessLogic.Services;
using Store.Presentation.Controllers.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Store.Shared.Enums.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Store.Presentation.Controllers
{
    public class AuthorController : BaseController
    {
        private readonly AuthorService _authorService;
        public AuthorController(AuthorService authorService, ILogger<AuthorController> logger) : base(logger)
        {
            _authorService = authorService;
        }

        [HttpGet("GetAuthors")]
        public async Task<List<AuthorModel>> GetAuthorModelsAsync()
        {
            return await _authorService.GetAuthorModelsAsync();
        }

        [HttpGet("GetAuthor")]
        public async Task<AuthorModel> GetAuthorModelAsync(long id)
        {
            return await _authorService.GetAuthorModelAsync(id);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost("CreateAuthor")]
        public async Task CreateAuthorAsync(AuthorModel authorModel)
        {
            await _authorService.CreateAuthorAsync(authorModel);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpDelete("DeleteAuthor")]

        public async Task DeleteAuthorAsync(long id)
        {
            await _authorService.DeleteAuthorAsync(id);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPost("UpdateAuthor")]
        public void UpdateAuthor(AuthorModel authorModel)
        {
            _authorService.UpdateAuthor(authorModel);
        }
    }
}
