using System.Collections.Generic;
using System.Threading.Tasks;
using ScarletMVC.Models.StarWars;

namespace ScarletMVC.Interfaces;

public interface IScarletApiServices
{
	Task<IEnumerable<Film>> GetFilms();
}
