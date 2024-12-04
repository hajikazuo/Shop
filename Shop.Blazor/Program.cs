using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Shop.Blazor;
using Shop.Blazor.Services.Api;
using Shop.Blazor.Services.Api.Implementation;
using Shop.Blazor.Services.Api.Interface;
using Shop.Blazor.Services.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("Shop.Api", options =>
{
    options.BaseAddress = new Uri("https://localhost:7229/");
}).AddHttpMessageHandler<CustomHttpHandler>();

builder.Services.AddScoped<CustomHttpHandler>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

await builder.Build().RunAsync();
