using Microsoft.Extensions.DependencyInjection;
using ScarletWebAPI.Interfaces;
using ScarletWebAPI.Repositories;

namespace ScarletWebAPI.Extensions;

public static class MediaStoreExtensions
{
	public static IServiceCollection RegisterMediaStoreRepositories(this IServiceCollection services)
	{
		services.AddScoped<IArtistRepository, ArtistRepository>();
		return services;
	}
}