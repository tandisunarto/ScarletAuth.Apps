using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScarletWebAPI.Areas.Xbox.Interfaces;
using ScarletWebAPI.Areas.Xbox.Services;

namespace ScarletWebAPI.IoC
{
    public static class HttpClientExtensions
    {
        public static void AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IXboxHttpClient, XboxHttpClient>(client => {
                client.BaseAddress = new Uri(configuration["OpenXBLApi:Uri"]);
                client.DefaultRequestHeaders.Add("X-Authorization", configuration["OpenXBLApi:Auth"]);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                // client.DefaultRequestHeaders.Add("User-Agent", ""); 
            });

            services.AddHttpClient<IStarWarsHttpClient, StarWarsHttpClient>();
        }
    }
}