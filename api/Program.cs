
using Domain.Repositories;

using IdentityModel;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;

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
    options.AddPolicy(corsPolicy, policy => policy.AllowAnyOrigin());
    //options.AddPolicy(corsPolicy, policy =>
    //{
    //    policy.WithOrigins("http://localhost:8080", "https://localhost:8080");
    //});
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

builder.Services.AddAuthentication("Bearer")
.AddIdentityServerAuthentication("Bearer", options =>
{
    options.ApiName = "newsy-api";
    options.Authority = "http://host.docker.internal:44342";
    options.RequireHttpsMetadata = false;
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

// Run migration (build database if it doens't exist)
using (var scope = app.Services.CreateScope())
{
    await using RepositoryDbContext dbContext = scope.ServiceProvider.GetRequiredService<RepositoryDbContext>();
    await dbContext.Database.MigrateAsync();
}
await app.RunAsync();