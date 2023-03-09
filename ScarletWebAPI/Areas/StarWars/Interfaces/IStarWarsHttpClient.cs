using System.Collections.Generic;
using System.Threading.Tasks;
using ScarletWebAPI.Areas.StarWars.Models;

namespace ScarletWebAPI.Areas.Xbox.Interfaces
{
    public interface IStarWarsHttpClient
    {
        Task<IEnumerable<T>> GetData<T>();
    }
}