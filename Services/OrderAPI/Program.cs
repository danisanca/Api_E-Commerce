using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OrderAPI.Data;
using OrderAPI.Data.Mapping.Dto;
using OrderAPI.Models;
using OrderAPI.RabbitMq.MessageConsumer;
using OrderAPI.RabbitMq.RabbitMQSender;
using OrderAPI.RabbitMq.RabbitMQSender.Interface;
using OrderAPI.Repository;
using OrderAPI.Repository.Interface;
using OrderAPI.Services;
using OrderAPI.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

//Confirgurando variavel base appsettings.json
var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

//Configuração do Sql
builder.Services.AddEntityFrameworkSqlServer()
    .AddDbContext<ApiContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
    );
//-

// Configurar o Bearer Token para autenticar no Identity Server
builder.Services.AddAuthentication(
    options =>
    {
        options.DefaultAuthenticateScheme = "Jwt";
        options.DefaultChallengeScheme = "Jwt";
    }).AddJwtBearer("Jwt", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            RequireExpirationTime = true,
            ClockSkew = TimeSpan.Zero,

            // ?? Estes valores devem ser IGUAIS aos do AuthServer
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };

    });
builder.Services.AddAuthorization();
//Configuração do AutoMapper
var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new DtoToModel());
});
IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

//Configurando repositorio
builder.Services.AddScoped<IOrderHeaderRepository, OrderHeaderRepository>();
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddHostedService<RabbitMQCheckoutConsumer>();

builder.Services.AddSingleton<IRabbitMQMessageSender, RabbitMQMessageSender>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "E-Commerce.OrderApi", Version = "v1" });
    c.EnableAnnotations();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Enter 'Bearer' [space] and your token!",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In= ParameterLocation.Header
                    },
                    new List<string> ()
                }
                });
});

var app = builder.Build();
var inicializeChannel = app.Services.CreateScope().ServiceProvider.GetService<IRabbitMQMessageSender>();

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
