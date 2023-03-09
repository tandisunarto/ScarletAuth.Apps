using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScarletWebAPI.Areas.Xbox.Interfaces;
using ScarletWebAPI.Areas.Xbox.Models.Games;

namespace ScarletWebAPI.Areas.Xbox.Controllers
{
    [ApiController]
    [Route("Xbox/[controller]")]
    // [Authorize]
    public class GamesController : ControllerBase
    {
        private readonly ILogger<GamesController> _logger;
        private readonly IXboxHttpClient xboxHttpClient;

        public GamesController(
            ILogger<GamesController> logger,
            IXboxHttpClient xboxHttpClient)
        {
            _logger = logger;
            this.xboxHttpClient = xboxHttpClient;
        }

        [HttpGet]
        public async Task<IEnumerable<Game>> Get()
        {
            var games = await xboxHttpClient.GetGames();
            return games;
        }

        [HttpGet("GetAchievements")]
        public async Task<IEnumerable<Achievement>> GetAchievements(int titleId)
        {
            var achievements = await xboxHttpClient.GetGamesAchievements(titleId);
            return achievements;
        }

        private JsonDocument GetJsonDocFromFile(string filename)
        {
            StreamReader reader = new StreamReader(filename);
            var json = reader.ReadToEnd();

            return JsonDocument.Parse(json);
        }
    }
}
