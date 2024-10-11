using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Shop.Blazor;
using Shop.Blazor.Services.API;
using Shop.Blazor.Services.API.Interfaces;
using Shop.Blazor.Util;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("ShopAPI", options =>
{
    options.BaseAddress = new Uri("https://localhost:7229/");
}).AddHttpMessageHandler<CustomHttpHandler>();

builder.Services.AddScoped<CustomHttpHandler>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<ICategoryService, CategoryService>();

await builder.Build().RunAsync();
