using DB;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

using FluentValidation.AspNetCore;
using reto_sofka_api_productos.Helpers;
using reto_sofka_api_productos.Services;
using reto_sofka_api_productos.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<EditProductValidator>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsImplementationPolicy", builder => builder.WithOrigins("*")
    .AllowAnyHeader());
});


builder.Services.AddDbContext<StoreContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"))
);


var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

//To be run once. It may override the DB
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<StoreContext>();
    context.Database.Migrate();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyCorsImplementationPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
