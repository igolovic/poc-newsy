using Contracts;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Services.Abstractions;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        private readonly ILogger<UserController> _logger;

        public UserController(IServiceManager serviceManager, ILogger<UserController> logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
        {
            var usersDto = await _serviceManager.UserService.GetAllAsync(cancellationToken);

            return Ok(usersDto);
        }

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetUserById(Guid userId, CancellationToken cancellationToken)
        {
            var userDto = await _serviceManager.UserService.GetByIdAsync(userId, cancellationToken);

            return Ok(userDto);
        }
    }
}