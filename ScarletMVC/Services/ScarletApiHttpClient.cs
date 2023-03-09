using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ScarletMVC.Interfaces;
using ScarletMVC.Models.StarWars;

namespace ScarletMVC.Services;

public class ScarletApiHttpClient
{
	public HttpClient httpClient { get; private set; }

	public ScarletApiHttpClient(HttpClient httpClient)
	{
		this.httpClient = httpClient;
		this.httpClient.BaseAddress = new Uri("https://localhost:55330");
		this.httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
	}

	public async Task<IEnumerable<T>> Get<T>(string uri)
	{
		var response = await httpClient.GetAsync(uri);
		response.EnsureSuccessStatusCode();
		var result = await response.Content.ReadAsStringAsync();
		var data = JsonSerializer.Deserialize<IEnumerable<T>>(result);
		return data;
	}

	// public async Task<IEnumerable<Film>> GetFilms()
	// {
	// 	var response = await httpClient.GetAsync("/StarWars/Films");
	// 	response.EnsureSuccessStatusCode();
	// 	var result = await response.Content.ReadAsStringAsync();
	// 	var films = JsonSerializer.Deserialize<IEnumerable<Film>>(result);
	// 	return films;
	// }

}