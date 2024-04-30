using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ScarletShared;
using ScarletWebAPI.Data;
using ScarletWebAPI.Extensions;
using ScarletWebAPI.Interfaces;
using ScarletWebAPI.IoC;
using ScarletWebAPI.Repositories;

namespace ScarletWebAPI;

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
        var chinookDb = Configuration.GetConnectionString("ChinookDb");
        services.AddDbContext<ChinookDbContext>(
            options => options.UseSqlite(chinookDb)
        );


        // services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
        //     .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme,
        //         options => {
        //             options.ApiName = "scarlet.web.api";
        //             options.Authority = Configuration[$"AuthorityUrl:{env.EnvironmentName}"];
        //         });

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
                options.Audience = "starwars.api";
                options.TokenValidationParameters = new() {
                    NameClaimType =  "given_name",
                    RoleClaimType = "role",
                    ValidTypes = new[] {"at+jwt"}
                };
                options.Authority = Configuration[$"AuthorityUrl:{env.EnvironmentName}"];
            });

        services.AddAuthorization(options => {
            options.AddPolicy("UserCanViewFilms", AuthorizationPolicies.CanViewFilms());
        });

        services.AddHttpClientServices(Configuration);

        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ScarletWebAPI", Version = "v1" });
        });

        services.RegisterMediaStoreRepositories();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ScarletWebAPI v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}