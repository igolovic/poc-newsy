using Contracts;

using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetArticles(CancellationToken cancellationToken)
        {
            var articlesDto = await _serviceManager.ArticleService.GetAllAsync(cancellationToken);

            return Ok(articlesDto);
        }

        [Authorize]
        [HttpGet("{articleId:guid}")]
        public async Task<IActionResult> GetArticleById(Guid articleId, CancellationToken cancellationToken)
        {
            var articleDto = await _serviceManager.ArticleService.GetByIdAsync(articleId, cancellationToken);

            return Ok(articleDto);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateArticle([FromBody] ArticleForCreationDto articleForCreationDto, CancellationToken cancellationToken)
        {
            var response = await _serviceManager.ArticleService.CreateAsync(articleForCreationDto, cancellationToken);

            return CreatedAtAction(nameof(GetArticleById), new { authorIds = response.ArticleUser, articleId = response.Id }, response);
        }
    }
}