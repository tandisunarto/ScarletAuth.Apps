using System.Collections.Generic;
using System.Threading.Tasks;
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
		var films = await httpClient.Get<Film>("/StarWars/Films");
		return films;

		// var response = await httpClient.GetAsync("/StarWars/Films");
		// response.EnsureSuccessStatusCode();
		// var result = await response.Content.ReadAsStringAsync();
		// var films = JsonSerializer.Deserialize<IEnumerable<Film>>(result);
		// return films;
	}
}