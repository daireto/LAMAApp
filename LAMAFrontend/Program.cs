using LAMAFrontend;
using LAMAFrontend.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    
    options.ProviderOptions.LoginMode = "redirect";
    
    var apiScopes = builder.Configuration.GetSection("Api:Scopes").Get<string[]>();
    if (apiScopes != null && apiScopes.Length > 0)
    {
        foreach (var scope in apiScopes)
        {
            options.ProviderOptions.DefaultAccessTokenScopes.Add(scope);
        }
    }
});

var apiBaseUrl = builder.Configuration["Api:BaseUrl"] ?? "";

builder.Services.AddHttpClient("LAMAAPI", 
    client => client.BaseAddress = new Uri(apiBaseUrl))
    .AddHttpMessageHandler(sp =>
    {
        var handler = sp.GetRequiredService<AuthorizationMessageHandler>()
            .ConfigureHandler(
                authorizedUrls: new[] { apiBaseUrl },
                scopes: builder.Configuration.GetSection("Api:Scopes").Get<string[]>() ?? Array.Empty<string>());
        return handler;
    });

builder.Services.AddScoped(sp => 
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    return httpClientFactory.CreateClient("LAMAAPI");
});

builder.Services.AddScoped<IMiembroService, MiembroService>();

await builder.Build().RunAsync();
