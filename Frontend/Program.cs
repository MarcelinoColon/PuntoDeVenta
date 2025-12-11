using FluentValidation;
using Frontend;
using Frontend.Client;
using Frontend.Validators;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var backendUrl = builder.Configuration["BackendUrl"];

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IValidator<ProductDto>, ProductDtoValidator>();

var culture = new CultureInfo("es-DO");
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient("Backend", http =>
{
    http.BaseAddress = new Uri(backendUrl);
});

builder.Services.AddScoped(sp =>
{
    var http = sp.GetRequiredService<IHttpClientFactory>().CreateClient("Backend");
    return new ApiClient(http);
});

await builder.Build().RunAsync();
