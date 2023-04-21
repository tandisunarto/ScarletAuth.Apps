using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScarletWebAPI.Areas.StarWars.Models;
using ScarletWebAPI.Areas.Xbox.Interfaces;

namespace ScarletWebAPI;

[ApiController]
[Route("/[controller]/[action]")]
public class StarWarsController : ControllerBase
{
	public IStarWarsHttpClient httpClient { get; private set; }

	public StarWarsController(IStarWarsHttpClient httpClient)
	{
		this.httpClient = httpClient;
	}

	[HttpGet]
	[Authorize]
	public async Task<IActionResult> Films()
	{
		var data = await httpClient.GetData<Film>();
		return Ok(data);
	}

	[HttpGet]
	public async Task<IActionResult> People()
	{
		var data = await httpClient.GetData<Person>();
		return Ok(data);
	}

	[HttpGet]
	public async Task<IActionResult> Planets()
	{
		var data = await httpClient.GetData<Planet>();
		return Ok(data);
	}

	[HttpGet]
	public async Task<IActionResult> Species()
	{
		var data = await httpClient.GetData<Species>();
		return Ok(data);
	}

	[HttpGet]
	public async Task<IActionResult> Starships()
	{
		var data = await httpClient.GetData<Starship>();
		return Ok(data);
	}
	[HttpGet]
	public async Task<IActionResult> Vehicles()
	{
		var data = await httpClient.GetData<Vehicle>();
		return Ok(data);
	}
}