using Contracts;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Services.Abstractions;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        private readonly ILogger<ArticleController> _logger;

        public ArticleController(IServiceManager serviceManager, ILogger<ArticleController> logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccounts(CancellationToken cancellationToken)
        {
            var articlesDto = await _serviceManager.AccountService.GetAllAsync(cancellationToken);

            return Ok(articlesDto);
        }

        [HttpGet("{articleId:guid}")]
        public async Task<IActionResult> GetAccountById(Guid articleId, CancellationToken cancellationToken)
        {
            var articleDto = await _serviceManager.AccountService.GetByIdAsync(articleId, cancellationToken);

            return Ok(articleDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] ArticleForCreationDto articleForCreationDto, CancellationToken cancellationToken)
        {
            var response = await _serviceManager.AccountService.CreateAsync(articleForCreationDto, cancellationToken);

            return CreatedAtAction(nameof(GetAccountById), new { authorIds = response.ArticleUser, articleId = response.Id }, response);
        }
    }
}