using CoffeStore.EcommerceApp.Adapters;
using CoffeStore.EcommerceApp.Adapters.Contracts;
using CoffeStore.Infra;
using CoffeStore.Infra.Context;
using CoffeStore.Infra.Repositories;
using CoffeStore.Infra.Settings;
using CoffeStore.Models.Contracts.Repositories;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<CoffeStoreDatabaseSettings>(builder.Configuration.GetSection(nameof(CoffeStoreDatabaseSettings)));

builder.Services.AddSingleton<MongoClient, CoffeStoreDbClient>();
builder.Services.AddSingleton<CoffeStoreDbContext>();

builder.Services.AddSingleton<IProductAdapter, ProductAdapter>();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();

builder.Services.AddControllersWithViews();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ecommerce API", Version = "v1" });
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
} else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce V1");
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
