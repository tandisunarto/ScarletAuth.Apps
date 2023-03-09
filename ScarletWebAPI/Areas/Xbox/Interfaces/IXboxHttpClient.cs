using System.Collections.Generic;
using System.Threading.Tasks;
using ScarletWebAPI.Areas.Xbox.Models.Games;

namespace ScarletWebAPI.Areas.Xbox.Interfaces
{
    public interface IXboxHttpClient
    {
        Task<IEnumerable<Game>> GetGames();
        Task<IEnumerable<Achievement>> GetGamesAchievements(int titleId);
    }
}