using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YandexCloud.BD.Postgres;
using YandexCloud.CORE.Repositories;
using YandexCloud.CORE.Services;

var builder = Host.CreateApplicationBuilder();
builder.Services.AddTransient<IBlService, BlService>();
builder.Services.AddScoped<IUoW, UoW>();
builder.Services.AddHttpClient();
using IHost host = builder.Build();

var baseCl = host.Services.GetService<IBlService>();
await baseCl.GetDataAsync();

await host.RunAsync();
