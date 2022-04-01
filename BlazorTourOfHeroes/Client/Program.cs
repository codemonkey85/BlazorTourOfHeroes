using BlazorTourOfHeroes.Client;
using BlazorTourOfHeroes.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<MessageService>();
builder.Services.AddScoped<HeroService>();
builder.Services.AddScoped<RefreshService>();

await builder.Build().RunAsync();
