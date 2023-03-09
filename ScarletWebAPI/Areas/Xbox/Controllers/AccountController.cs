using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScarletWebAPI.Areas.Xbox.Interfaces;

namespace ScarletWebAPI.Areas.Xbox.Controllers
{
    [ApiController]
    [Route("Xbox/[controller]")]
    // [Authorize]
    public class AccountController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<AccountController> _logger;
        private readonly IXboxHttpClient xboxHttpClient;

        public AccountController(
            ILogger<AccountController> logger,
            IXboxHttpClient xboxHttpClient)
        {
            _logger = logger;
            this.xboxHttpClient = xboxHttpClient;
        }

        [HttpGet]
        public IActionResult Get()
        {
            // var rng = new Random();
            // return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            // {
            //     Date = DateTime.Now.AddDays(index),
            //     TemperatureC = rng.Next(-20, 55),
            //     Summary = Summaries[rng.Next(Summaries.Length)]
            // })
            // .ToArray();

            return Content("Hello World");
        }
    }
}
