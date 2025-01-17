using Chilaqueria_API.Controllers.BussinessLogic;
using Chilaqueria_API.Datos;
using Chilaqueria_API.Repositories;
using Microsoft.EntityFrameworkCore;

//using WebApiEntityFrameworkDockerSqlServer.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ChilaqueriaDBContext>(options =>
options.UseSqlServer(
    builder.Configuration.GetConnectionString("ChilaqueríaCS")
    ));

builder.Services.AddScoped<AccountLogic>();
builder.Services.AddScoped<ProductsLogic>();

builder.Services.AddScoped<IAccountService, AccountService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
