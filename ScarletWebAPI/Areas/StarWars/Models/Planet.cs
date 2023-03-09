using System.Collections.Generic;

namespace ScarletWebAPI.Areas.StarWars.Models;

public class Planet
{
	public string name { get; set; }
	public string diameter { get; set; }
	public string rotation_period { get; set; }
	public string orbital_period { get; set; }
	public string gravity { get; set; }
	public string population { get; set; }
	public string climate { get; set; }
	public string terrain { get; set; }
	public string surface_water { get; set; }
	public IEnumerable<string> residents { get; set; }
	public IEnumerable<string> films { get; set; }
	public string url { get; set; }
	public string created { get; set; }
	public string edited { get; set; }
}