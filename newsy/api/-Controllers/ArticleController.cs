//using Contracts;

//using Microsoft.AspNetCore.Mvc;

//using Services.Abstractions;

//namespace api.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class ArticleController : ControllerBase
//    {
//        private readonly IServiceManager _serviceManager;

//        private readonly ILogger<ArticleController> _logger;

//        public ArticleController(IServiceManager serviceManager, ILogger<ArticleController> logger)
//        {
//            _serviceManager = serviceManager;
//            _logger = logger;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAccounts(CancellationToken cancellationToken)
//        {
//            var articlesDto = await _serviceManager.AccountService.GetAllAsync(cancellationToken);

//            return Ok(articlesDto);
//        }

//        [HttpGet("{articleId:guid}")]
//        public async Task<IActionResult> GetAccountById(Guid articleId, CancellationToken cancellationToken)
//        {
//            var articleDto = await _serviceManager.AccountService.GetByIdAsync(articleId, cancellationToken);

//            return Ok(articleDto);
//        }

//        [HttpPost]
//        public async Task<IActionResult> CreateAccount([FromBody] ArticleForCreationDto articleForCreationDto, CancellationToken cancellationToken)
//        {
//            var response = await _serviceManager.AccountService.CreateAsync(articleForCreationDto, cancellationToken);

//            return CreatedAtAction(nameof(GetAccountById), new { authorIds = response.AuthorIds, articleId = response.Id }, response);
//        }

//        //[HttpGet(Name = "GetWeatherForecast")]
//        //public IEnumerable<ArticleDto> Get()
//        //{
//        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
//        //    {
//        //        Date = DateTime.Now.AddDays(index),
//        //        TemperatureC = Random.Shared.Next(-20, 55),
//        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
//        //    })
//        //    .ToArray();
//        //}
//    }
//}