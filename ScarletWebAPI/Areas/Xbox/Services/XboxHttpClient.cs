using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ScarletWebAPI.Areas.Xbox.Interfaces;
using ScarletWebAPI.Areas.Xbox.Models.Games;

namespace ScarletWebAPI.Areas.Xbox.Services
{
    public class XboxHttpClient : IXboxHttpClient
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public XboxHttpClient(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public async Task<IEnumerable<Game>> GetGames()
        {
            // var xuid = configuration["XboxApi:UId"];
            // var response = await httpClient.GetAsync($"{xuid}/titlehub-achievement-list");
            var response = await httpClient.GetAsync("achievements");
            response.EnsureSuccessStatusCode ();
			var responseJson = await response.Content.ReadAsStringAsync ();

            List<Game> games = new List<Game>();

            var jsonDoc = JsonDocument.Parse(responseJson);
            var root = jsonDoc.RootElement;

            var titleElement = root.GetProperty("titles");
            var count = titleElement.GetArrayLength();

            foreach(JsonElement title in titleElement.EnumerateArray())
            {
                title.TryGetProperty("name", out JsonElement nameElement);
                title.TryGetProperty("titleId", out JsonElement titleIdElement);
                title.TryGetProperty("displayImage", out JsonElement displayImageElement);
                title.TryGetProperty("achievement", out JsonElement achievementElement);  

                achievementElement.TryGetProperty("totalGamerscore", out JsonElement totalGamerscoreElement);
                achievementElement.TryGetProperty("currentGamerscore", out JsonElement currentGamerscoreElement);

                games.Add(new Game {
                    TitleId = Int32.Parse(titleIdElement.GetString()),
                    TitleName = nameElement.GetString(),
                    DisplayImage = displayImageElement.GetString(),
                    MaxGameScore = totalGamerscoreElement.GetInt32(),
                    CurrentGameScore = currentGamerscoreElement.GetInt32(),
                });
            }

            return games;
        }

        public async Task<IEnumerable<Achievement>> GetGamesAchievements(int titleId)
        {
            // var xuid = configuration["XboxApi:UId"];
            // var response = await httpClient.GetAsync($"{xuid}/achievements/{titleId}");
            var response = await httpClient.GetAsync($"achievements/title/{titleId}");
            response.EnsureSuccessStatusCode ();
			var responseJson = await response.Content.ReadAsStringAsync();

            var jsonDoc = JsonDocument.Parse(responseJson);
            var root = jsonDoc.RootElement;

            var achievements = new List<Achievement>();

            foreach(var achievement in root.EnumerateArray())
            {
                achievement.TryGetProperty("name", out JsonElement nameElement);
                achievement.TryGetProperty("description", out JsonElement descriptionElement);
                achievement.TryGetProperty("rewards", out JsonElement rewardsElement);

                rewardsElement[0].TryGetProperty("value", out JsonElement rewardsValueElement);
                
                achievements.Add(
                    new Achievement {
                        Name = nameElement.GetString(),
                        Description = descriptionElement.GetString(),
                        Reward = rewardsValueElement.GetInt32()
                    }
                );
                Console.WriteLine($"{nameElement.GetString()} - {descriptionElement.GetString()} ({rewardsValueElement.GetInt32()})");
            }

            return achievements;
        }
    }
}