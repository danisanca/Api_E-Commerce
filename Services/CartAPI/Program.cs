using System;
using System.Reflection;
using System.Text;
using AutoMapper;
using CartAPI.Data;
using CartAPI.Data.Mapping.Dtos;
using CartAPI.Models;
using CartAPI.Repositories;
using CartAPI.Services;
using CartAPI.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SharedBase.Repository;

var builder = WebApplication.CreateBuilder(args); 
//Confirgurando variavel base appsettings.json
var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json") .Build(); 
//Configuração do Sql
builder.Services.AddEntityFrameworkSqlServer() 
    .AddDbContext<ApiContext>( options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")) ); 
//- 

// Configurar o Bearer Token para autenticar no Identity Server
builder.Services.AddAuthentication( options => 
{ options.DefaultAuthenticateScheme = "Jwt"; options.DefaultChallengeScheme = "Jwt"; })
    .AddJwtBearer("Jwt", options => 
    { options.TokenValidationParameters = new TokenValidationParameters { 
        ValidateIssuer = true, 
        ValidateAudience = true, 
        ValidateIssuerSigningKey = true,
        RequireExpirationTime = true, 
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["Jwt:Issuer"], 
        ValidAudience = builder.Configuration["Jwt:Audience"], 
        IssuerSigningKey = new SymmetricSecurityKey( 
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) }; 
    }); 
builder.Services.AddAuthorization(); 

//Configuração do AutoMapper
var config = new AutoMapper.MapperConfiguration(cfg => { 
    cfg.AddProfile(new DtoToModel()); }); 
    IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper); 
//- 

//Configurando repositorio
builder.Services.AddScoped(typeof(IBaseRepository<CartHeader>), typeof(BaseRepository<CartHeader, ApiContext>));
builder.Services.AddScoped(typeof(IBaseRepository<CartDetail>), typeof(BaseRepository<CartDetail, ApiContext>)); 
builder.Services.AddScoped<ICartRepository, CartRepository>(); 
builder.Services.AddScoped<ICartService, CartService>(); 
builder.Services.AddSingleton<IRabbitMqMessageSender, RabbitMqMessageSender>();

// Add services to the container.
builder.Services.AddControllers(); 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "E-Commerce.CartApi", Version = "v1" });
    c.EnableAnnotations();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        Description = @"Enter 'Bearer' [space] and your token!", 
        Name = "Authorization", 
        In = ParameterLocation.Header, 
        Type = SecuritySchemeType.ApiKey, 
        Scheme = "Bearer" 
    }); 
    c.AddSecurityRequirement(new OpenApiSecurityRequirement { 
        { 
            new OpenApiSecurityScheme{ 
            Reference = new OpenApiReference { 
                Type = ReferenceType.SecurityScheme, 
                Id = "Bearer" 
            }, 
            Scheme = "oauth2", 
            Name = "Bearer", 
            In= ParameterLocation.Header 
            },
            new List<string> () 
        } }); 
});
//CorsConfig
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:4200") // endereço do Angular
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials(); // se estiver usando autenticação com cookies ou tokens
        });
});
var app = builder.Build();
app.UseCors("AllowAngularApp");
var inicializeChannel = app.Services.CreateScope().ServiceProvider.GetService<IRabbitMqMessageSender>(); 

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

inicializeChannel.InitializeRabbitMq(); 

app.Run();