using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace ScarletMVC;

public class AuthTokenHttpMessageHandler : DelegatingHandler
{
  private readonly IHttpContextAccessor httpContextAccessor;

  // TODO: use IOption to inject config for the HttpClient
  public AuthTokenHttpMessageHandler(IHttpContextAccessor httpContextAccessor)
  {
    this.httpContextAccessor = httpContextAccessor;
  }

  protected override async Task<HttpResponseMessage> SendAsync(
    HttpRequestMessage request,
    CancellationToken cancellationToken
  )
  {
    var token = this.httpContextAccessor.HttpContext.GetTokenAsync("access_token").GetAwaiter().GetResult();
    request.Headers.Add("Authorization", $"bearer {token}");
    HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

    return response;
  }
}