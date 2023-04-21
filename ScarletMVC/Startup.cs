using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScarletMVC.Interfaces;
using ScarletMVC.Models;
using ScarletMVC.Services;

namespace ScarletMVC
{
    public class Startup
    {
        private readonly IWebHostEnvironment env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            this.env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddControllersWithViews();

            services.AddAuthentication(options => {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => {
                options.AccessDeniedPath = "/Account/AccessDenied";
            })
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options => {
                options.Authority = Configuration[$"InteractiveServiceSettings:{env.EnvironmentName}:AuthorityUrl"];
                options.ClientId = Configuration[$"InteractiveServiceSettings:{env.EnvironmentName}:ClientId"];
                options.ClientSecret = Configuration[$"InteractiveServiceSettings:{env.EnvironmentName}:ClientSecret"];

                options.ResponseType = "code";
                options.UsePkce = true;
                options.ResponseMode = "query";

                var scopes = Configuration.GetSection($"InteractiveServiceSettings:{env.EnvironmentName}:Scopes").Get<List<string>>();
                foreach(var scope in scopes)
                {
                    options.Scope.Add(scope);
                }
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;

                // remove the claim "aud" from the IDP excluded claim list
                options.ClaimActions.Remove("aud");

                options.ClaimActions.MapJsonKey("role", "role");
                options.ClaimActions.MapJsonKey("email", "email");

                options.TokenValidationParameters = new()
                {
                    NameClaimType = "name",
                    RoleClaimType = "role"
                };

            });

            services.Configure<OtherSettings>(Configuration.GetSection($"OtherSettings:{env.EnvironmentName}"));
            services.Configure<IdentityServerSettings>(Configuration.GetSection($"IdentityServerSettings:{env.EnvironmentName}"));
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IScarletApiServices, ScarletApiServices>();
            // services.AddScoped<ScarletApiHttpClient>();
            services.AddHttpClient<ScarletApiHttpClient>();

            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
