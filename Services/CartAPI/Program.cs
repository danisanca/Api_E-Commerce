using AutoMapper;
using CartAPI.Data;
using CartAPI.Data.Mapping.Dtos;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

//Configuração do Sql
builder.Services.AddEntityFrameworkSqlServer()
    .AddDbContext<ApiContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
    );

//-
//Configuração do AutoMapper
var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new DtoToModel());
});
IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);
//-

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
