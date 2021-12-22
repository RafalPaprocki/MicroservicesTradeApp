using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TradingStatisticAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class StatisticController : ControllerBase
    {
        private readonly ILogger<StatisticController> _logger;

        public StatisticController(ILogger<StatisticController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetStatistic()
        {
            // TODO implement
            _logger.LogInformation(nameof(StatisticController) + " " + nameof(GetStatistic) + "invoked");
            return Ok();
        }
    }
}