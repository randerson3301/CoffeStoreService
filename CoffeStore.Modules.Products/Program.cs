using CoffeStore.Common.ErrorContext;
using CoffeStore.Modules.Products.Application;
using CoffeStore.Modules.Products.Application.Adapters;
using CoffeStore.Modules.Products.Application.Adapters.Contracts;
using CoffeStore.Modules.Products.Application.Commands;
using CoffeStore.Modules.Products.Application.Validators;
using CoffeStore.Modules.Products.Domain.Contracts;
using CoffeStore.Modules.Products.Infra;
using CoffeStore.Modules.Products.Infra.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProductContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        });
});

builder.Services.AddSingleton<IProductAdapter, ProductAdapter>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IErrorContext, ErrorContext>();

builder.Services.AddScoped<IValidator<CreateProductCommand>, CreateProductCommandValidator>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

ProductEndpoints.Map(app);

app.Run();