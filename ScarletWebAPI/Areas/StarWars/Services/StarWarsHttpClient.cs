using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ScarletWebAPI.Areas.StarWars.Models;
using ScarletWebAPI.Areas.Xbox.Interfaces;
using ScarletWebAPI.Areas.Xbox.Models.Games;

namespace ScarletWebAPI.Areas.Xbox.Services
{
    public class StarWarsHttpClient : IStarWarsHttpClient
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public StarWarsHttpClient(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;

            this.httpClient.BaseAddress = new Uri(configuration["StarWarsApi"]);
            this.httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        protected string EntityName<T>()
        {
            if (typeof(T) == typeof(Person)) 
                return "people";
            else if (typeof(T) == typeof(Planet)) 
                return "planets";
            else if (typeof(T) == typeof(Species)) 
                return "species";
            else if (typeof(T) == typeof(Starship)) 
                return "starships";
            else if (typeof(T) == typeof(Vehicle))
                return "vehicles"; 
            else
                return "films";
        }

        public async Task<IEnumerable<T>> GetData<T>()
        {
            var t = typeof(T);
            var response = await httpClient.GetAsync(EntityName<T>());
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var jsonDoc = JsonDocument.Parse(content).RootElement.GetProperty("results");            
            var data = JsonSerializer.Deserialize<IEnumerable<T>>(jsonDoc);
            return data;
        }
    }
}