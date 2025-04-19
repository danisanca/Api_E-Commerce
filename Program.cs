
using ApiEstoque.Data;
using ApiEstoque.Data.Mapping.Dtos;
using ApiEstoque.Repository;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services;
using ApiEstoque.Services.Interface;
using ApiEstoque.Services.Security;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
namespace ApiEstoque
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            
            MercadoPago.Config.MercadoPagoConfig.AccessToken = builder.Configuration.GetValue<string>("MercadoPago:AccessToken");
            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //Mercardo Pago
           
            //Configuração do AutoMapper
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoToModelProfile());
            });
            IMapper mapper = config.CreateMapper();
            builder.Services.AddSingleton(mapper);
            //-
            //Configuração do Sql
            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<ApiContext>(
                    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
                );
            //-Configurando os Repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            builder.Services.AddScoped<ICategoriesService, CategoriesService>();
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<IImageRepository, ImageRepository>();
            builder.Services.AddScoped<IShopRepository, ShopRepository>();
            builder.Services.AddScoped<IShopService, ShopService>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IStockRepository, StockRepository>();
            builder.Services.AddScoped<IStockService, StockService>();
            builder.Services.AddScoped<IHistoryMovimentRepository, HistoryMovimentRepository>();
            builder.Services.AddScoped<IHistoryMovimentService, HistoryMovimentService>();
            builder.Services.AddScoped<IHistoryPurchaseRepository, HistoryPurchaseRepository>();
            builder.Services.AddScoped<IHistoryPurchaseService, HistoryPurchaseService>();
            builder.Services.AddScoped<IEvidenceRepository, EvidenceRepository>();
            builder.Services.AddScoped<IEvidenceService, EvidenceService>();
            builder.Services.AddScoped<IScoreProductRepository, ScoreProductRepository>();
            builder.Services.AddScoped<IScoreProductService, ScoreProductService>();
            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
            builder.Services.AddScoped<IDiscountService, DiscountService>();
            builder.Services.AddScoped<IAddressRepository, AddressRepository>();
            builder.Services.AddScoped<IAddressService, AddressService>();
            //-
            //-- Configurando o Token
            var signingConfigurations = new SigningConfigurations();
            builder.Services.AddSingleton(signingConfigurations);
            
            builder.Services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = configuration["Audience"];
                paramsValidation.ValidIssuer = configuration["Issuer"];
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });
            builder.Services.AddAuthorization(auth =>
            {
                
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build());
            });
            //Configurando Autenticação no Swagger
            builder.Services.AddSwaggerGen(c =>
            {
                
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Entre com o Token Jwt",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                    
                    
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        }, new List<String>()
                    }
                });
            });
            //Configurando Cors
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
            //-
            var app = builder.Build();


            app.UseCors();
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
        }
    }
}
