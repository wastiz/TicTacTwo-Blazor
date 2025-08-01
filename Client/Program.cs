using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;

using Client;
using Client.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//Authorization Provider
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();

//Header Handler
var apiBaseUrl = builder.HostEnvironment.IsDevelopment() ? "http://localhost:3005" : null;

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Local", options.ProviderOptions);
});

builder.Services.AddScoped<AuthHeaderHandler>();
builder.Services.AddHttpClient("AuthHttpClient", client =>
    {
        client.BaseAddress = apiBaseUrl is not null ? new Uri(apiBaseUrl) : null;
    })
    .AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddScoped(sp => 
    new HttpClient { 
        BaseAddress = apiBaseUrl is not null ? new Uri(apiBaseUrl) : null 
    });



//For local storage
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
