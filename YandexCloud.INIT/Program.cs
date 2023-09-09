using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YandexCloud.BD.Ozon;
using YandexCloud.BD.Postgres;
using YandexCloud.CORE.Repositories;
using YandexCloud.CORE.Services;
using YandexCloud.INIT;

var builder = Host.CreateApplicationBuilder();
builder.Services.AddTransient<IBlService, BlService>();
builder.Services.AddTransient<IOzonFullData, WebOzonData>();
builder.Services.AddScoped<IUoW, UoW>();
builder.Services.AddTransient<IRequestReader, RequestReader>();
builder.Services.AddHttpClient();
using IHost host = builder.Build();

var reader = host.Services.GetService<IRequestReader>();
var requestModel = reader.Read();

var baseCl = host.Services.GetService<IBlService>();
await baseCl.GetDataAsync(requestModel);

await host.RunAsync();
