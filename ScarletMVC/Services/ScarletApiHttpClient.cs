using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using ScarletMVC.Interfaces;
using ScarletMVC.Models.StarWars;

namespace ScarletMVC.Services;

public class ScarletApiHttpClient
{
  private readonly IHttpContextAccessor httpContextAccessor;

  public HttpClient httpClient { get; private set; }

	public ScarletApiHttpClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
	{
		this.httpContextAccessor = httpContextAccessor;
		var token = this.httpContextAccessor.HttpContext.GetTokenAsync("access_token").GetAwaiter().GetResult();
		this.httpClient = httpClient;
		this.httpClient.BaseAddress = new Uri("https://localhost:55330");
		this.httpClient.DefaultRequestHeaders.Add("Accept", "application/json");		
		this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
	}

	public async Task<IEnumerable<T>> Get<T>(string uri)
	{
		var response = await httpClient.GetAsync(uri);
		response.EnsureSuccessStatusCode();
		var result = await response.Content.ReadAsStringAsync();
		var data = JsonSerializer.Deserialize<IEnumerable<T>>(result);
		return data;
	}

	public async Task<HttpResponseMessage> Get(string uri)
	{
		return await httpClient.GetAsync(uri);
	}
}