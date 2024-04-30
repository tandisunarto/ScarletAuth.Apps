using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScarletShared.Models;
using ScarletWebAPI.Interfaces;

namespace ScarletWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // [Authorize]
    public class ArtistController : ControllerBase
    {
        private readonly ILogger<ArtistController> logger;
        private readonly IArtistRepository repository;

        public ArtistController(
          ILogger<ArtistController> logger,
          IArtistRepository repository
          )
        {
            this.logger = logger;
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<Artist> Get()
        {
			var artists = repository.Get();
			return artists;
        }

        [HttpGet("/ArtistId")]
        public Artist GetById(int id)
        {
			var artist = repository.GetById(id);
			return artist;
        }

        [HttpPut]
        public void Update(Artist artist)
        {
			repository.Update(artist);
        }
    }
}
