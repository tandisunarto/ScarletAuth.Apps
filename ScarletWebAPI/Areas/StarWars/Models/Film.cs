using System;
using System.Collections.Generic;

namespace ScarletWebAPI.Areas.StarWars.Models;

public class Film : BaseType
{
	public string title { get; set; }
	public int episode_id { get; set; }
	public string opening_crawl { get; set; }
	public string director { get; set; }
	public string producer { get; set; }
	public DateTime release_date { get; set; }
	public IEnumerable<string> species { get; set; }
	public IEnumerable<string> starships { get; set; }
	public IEnumerable<string> vehicles { get; set; }
	public IEnumerable<string> characters { get; set; }
	public IEnumerable<string> planets { get; set; }	
}