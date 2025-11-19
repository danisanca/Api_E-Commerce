
using System.Net;
using System.Reflection;
using System.Text;
using ApiEstoque.Data;
using ApiEstoque.Data.Mapping.Dtos;
using ApiEstoque.Initializer;
using ApiEstoque.Models;
using ApiEstoque.Repository;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services;
using ApiEstoque.Services.Interface;
using ApiEstoque.Services.Security;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SharedBase.Repository;
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
            var jwtSettings = builder.Configuration.GetSection("Jwt");

            //Mercardo Pago
            MercadoPago.Config.MercadoPagoConfig.AccessToken = builder.Configuration.GetValue<string>("MercadoPago:AccessToken");
            //-

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
            //-

            //Configurando Identity
            builder.Services.AddIdentity<UserModel, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<ApiContext>()
            .AddDefaultTokenProviders();
            //-
            
            //Configurando Authenticação
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Jwt";
                options.DefaultChallengeScheme = "Jwt";
            }).AddJwtBearer("Jwt", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value)),
                };
            }).AddCookie("Cookies");
            //-

            //-Configurando os Repositories Base
            builder.Services.AddScoped(typeof(IBaseRepository<AddressModel>), typeof(BaseRepository<AddressModel, ApiContext>));
            builder.Services.AddScoped(typeof(IBaseRepository<CategoriesModel>), typeof(BaseRepository<CategoriesModel, ApiContext>));
            builder.Services.AddScoped(typeof(IBaseRepository<DiscountModel>), typeof(BaseRepository<DiscountModel, ApiContext>));
            builder.Services.AddScoped(typeof(IBaseRepository<HistoryMovimentModel>), typeof(BaseRepository<HistoryMovimentModel, ApiContext>));
            builder.Services.AddScoped(typeof(IBaseRepository<ImageModel>), typeof(BaseRepository<ImageModel, ApiContext>));
            builder.Services.AddScoped(typeof(IBaseRepository<ProductModel>), typeof(BaseRepository<ProductModel, ApiContext>));
            builder.Services.AddScoped(typeof(IBaseRepository<ShopModel>), typeof(BaseRepository<ShopModel, ApiContext>));
            builder.Services.AddScoped(typeof(IBaseRepository<StockModel>), typeof(BaseRepository<StockModel, ApiContext>));
            //-Configurando os Repositories
            builder.Services.AddTransient<IAuthService, AuthService>();
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
            builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
            builder.Services.AddScoped<IDiscountService, DiscountService>();
            builder.Services.AddScoped<IAddressRepository, AddressRepository>();
            builder.Services.AddScoped<IAddressService, AddressService>();
            builder.Services.AddScoped<IDbInitializer, DbInitializer>();
            //-
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
            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "E-Commerce.MainApi", Version = "v1" });
                c.EnableAnnotations(); // para [SwaggerOperation]
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
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
            app.UseCors("AllowAngularApp");
            var initializer = app.Services.CreateScope().ServiceProvider.GetService<IDbInitializer>();

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

            initializer.Initialize();
            app.Run();
        }
    }
}
