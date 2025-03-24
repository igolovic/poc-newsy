using System.Text;
using Domain.Repositories;

using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Persistence.Repositories;

using Services;
using Services.Abstractions;

using Web.Middleware;

var corsPolicy = "corsPolicy";
var builder = WebApplication.CreateBuilder(args);

IdentityModelEventSource.ShowPII = true;

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy, policy => policy.AllowAnyOrigin().AllowAnyHeader());
});

builder.Services
.AddControllers()
.AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddDbContextPool<RepositoryDbContext>(dbContextbuilder =>
{
    var connectionString = builder.Configuration.GetConnectionString("Database");
    dbContextbuilder.UseNpgsql(connectionString);
});
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddIdentityServerAuthentication(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.ApiName = "newsy-api";
    options.Authority = "https://host.docker.internal:44343";
    options.JwtBackChannelHandler = new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback =
              (message, certificate, chain, sslPolicyErrors) => true
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("newsy-api-scope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim(JwtClaimTypes.Scope, new string[] { "newsy-api.read", "newsy-api.write" });
    });
});

var app = builder.Build();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Lax
});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseCors(corsPolicy);
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization("newsy-api-scope");

// Run migration (build database if it doesn't exist)
using (var scope = app.Services.CreateScope())
{
    await using RepositoryDbContext dbContext = scope.ServiceProvider.GetRequiredService<RepositoryDbContext>();
    //await dbContext.Database.MigrateAsync();
}
await app.RunAsync();

public class Api
{
    static public SymmetricSecurityKey GetSignInKey()
    {
        const string secretKey = "very_long_very_secret_secret";
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        return signingKey;
    }

    static public string GetIssuer()
    {
        return "issuer";
    }

    static public string GetAudience()
    {
        return "audience";
    }
}