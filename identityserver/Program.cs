using identityserver.IdentityConfiguration;

//var corsPolicy = "allowAnyOrigin";
var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(corsPolicy, policy => policy.AllowAnyOrigin());
//});

builder.Services.AddIdentityServer()
    .AddInMemoryClients(Clients.Get())
    .AddInMemoryIdentityResources(Resources.GetIdentityResources())
    .AddInMemoryApiResources(Resources.GetApiResources())
    .AddInMemoryApiScopes(Scopes.GetApiScopes())
    .AddTestUsers(Users.Get())
    .AddDeveloperSigningCredential();

builder.Services.AddControllersWithViews();

var app = builder.Build();

//app.UseCookiePolicy(new CookiePolicyOptions
//{
//    MinimumSameSitePolicy = SameSiteMode.Lax
//});

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());

//app.UseCors(allowAnyOrigin);
app.Run();
