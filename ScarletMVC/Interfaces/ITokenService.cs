using System.Threading.Tasks;
using IdentityModel.Client;

namespace ScarletMVC.Interfaces
{
    public interface ITokenService
    {
        Task<TokenResponse> GetToken(string scope);
    }
}