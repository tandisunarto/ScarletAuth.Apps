using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using ScarletMVC.Helpers;
using ScarletMVC.Interfaces;
using ScarletMVC.Models.StarWars;

namespace ScarletMVC.Services;

public class ScarletApiServices : IScarletApiServices
{
	public ScarletApiHttpClient httpClient { get; private set; }
	public ScarletApiServices(ScarletApiHttpClient httpClient)
	{
		this.httpClient = httpClient;
	}

	public async Task<IEnumerable<Film>> GetFilms()
	{
		var response = await httpClient.Get("/StarWars/Films");
		response.EnsureSuccessStatusCode();
		var result = await response.Content.ReadAsStringAsync();
		var films = JsonSerializer.Deserialize<IEnumerable<Film>>(result);
		return films;
	}

	public async Task<IEnumerable<Vehicle>> GetVehicles()
	{
		var response = await httpClient.Get("/StarWars/Vehicles");
		response.EnsureSuccessStatusCode();
		var result = await response.Content.ReadAsStringAsync();
		var vehicles = JsonSerializer.Deserialize<IEnumerable<Vehicle>>(result);
		return vehicles;
	}
}