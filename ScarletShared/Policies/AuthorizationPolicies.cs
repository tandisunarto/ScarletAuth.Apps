using Microsoft.AspNetCore.Authorization;

namespace ScarletShared;

public static class AuthorizationPolicies
{
  public static AuthorizationPolicy CanViewFilms()
  {
    return new AuthorizationPolicyBuilder()
      .RequireAuthenticatedUser()
      .RequireClaim("country", "uk")
      .RequireRole("nerd")
      .Build();
  }
}