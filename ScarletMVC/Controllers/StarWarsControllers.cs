using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScarletMVC.Interfaces;
using ScarletMVC.Services;

namespace ScarletMVC.Controllers;

public class StarWarsController : Controller
{
  	public IScarletApiServices scarletApiServices { get; private set; }
	
	public StarWarsController(IScarletApiServices scarletApiServices)
	{
		this.scarletApiServices = scarletApiServices;
	}

  	public async Task<IActionResult> Films()
	{
		var films = await scarletApiServices.GetFilms();
		return View(films);
	}
}