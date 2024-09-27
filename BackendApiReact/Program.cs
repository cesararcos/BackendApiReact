using BackendApiReact.Application;
using BackendApiReact.Application.Contracts;
using BackendApiReact.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<GestoresDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("database")));

builder.Services.AddTransient<IGestoresAppService, GestoresAppService>();
builder.Services.AddTransient<IGestoresSupervisorAppService, GestoresSupervisorAppService>();
builder.Services.AddTransient<IGestoresEmpleadoAppService, GestoresEmpleadoAppService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(options =>
{
    options.WithOrigins("http://localhost:3000");
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
