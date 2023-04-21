using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ScarletMVC.Models.StarWars;

public class Vehicle
{
	[JsonPropertyName("episode_id")]
	public int Id { get; set; }

	[JsonPropertyName("title")]
	public string Title { get; set; }

	[JsonPropertyName("opening_crawl")]
	public string OpeningCrawl { get; set; }

	[JsonPropertyName("director")]
	public string Director { get; set; }

	[JsonPropertyName("producer")]
	public string Producer { get; set; }

	[JsonPropertyName("release_date")]
	public DateTime ReleaseDate { get; set; }

	[JsonPropertyName("species")]
	public IEnumerable<string> Species { get; set; }

	[JsonPropertyName("starships")]
	public IEnumerable<string> Starships { get; set; }
	
	[JsonPropertyName("vehicles")]
	public IEnumerable<string> Vehicles { get; set; }
	
	[JsonPropertyName("characters")]
	public IEnumerable<string> Characters { get; set; }

	[JsonPropertyName("planets")]
	public IEnumerable<string> Planets { get; set; }

	[JsonPropertyName("name")]
	public string Name { get; set; }

	[JsonPropertyName("url")]
	public string URL { get; set; }
}