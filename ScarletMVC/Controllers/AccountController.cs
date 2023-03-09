using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace ScarletMVC.Controllers;

public class AccountController : Controller
{

	public IActionResult Login()
	{
		return Challenge(
			new AuthenticationProperties {
				RedirectUri = $"{(HttpContext.Request.IsHttps ? "https" : "http")}://{HttpContext.Request.Host.Value}"
			},
			OpenIdConnectDefaults.AuthenticationScheme);
	}

	public IActionResult Logout()
	{
		return SignOut(
		new AuthenticationProperties {
			RedirectUri = $"{(HttpContext.Request.IsHttps ? "https" : "http")}://{HttpContext.Request.Host.Value}",
		},
		new[] {
			CookieAuthenticationDefaults.AuthenticationScheme,
			OpenIdConnectDefaults.AuthenticationScheme
		});
	} 

	public IActionResult AccessDenied()
	{
		return View();
	}
}