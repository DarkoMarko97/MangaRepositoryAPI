using MangaRepositoryAPI.Data;
using MangaRepositoryAPI.DTO;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connStr = builder.Configuration.GetConnectionString("Default");
builder.Services.AddSqlServer<MangaRepoDbContext>(connStr);
builder.Services.AddSingleton<Mapper>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
