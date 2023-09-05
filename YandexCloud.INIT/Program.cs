using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YandexCloud.BD;
using YandexCloud.CORE;

var builder = Host.CreateApplicationBuilder();
builder.Services.AddTransient<IBL, BL>();
builder.Services.AddTransient<IDB, DB>();
builder.Services.AddHttpClient();
using IHost host = builder.Build();

var baseCl = host.Services.GetService<IBL>();
baseCl.BasisLogik();

await host.RunAsync();
