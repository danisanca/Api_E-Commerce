using System.Security.Claims;
using ApiEstoque.Constants;
using ApiEstoque.Data;
using ApiEstoque.Dto.Adress;
using ApiEstoque.Models;
using ApiEstoque.Services.Interface;
using Microsoft.AspNetCore.Identity;
using static ApiEstoque.Constants.Roles;

namespace ApiEstoque.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApiContext _context;
        private readonly UserManager<UserModel> _user;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAddressService _address;

        public DbInitializer(ApiContext context, IAddressService address, UserManager<UserModel> user, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _user = user;
            _roleManager = roleManager;
            _address = address;
        }

        public void Initialize()
        {
            InitializeCategories();
            InitializeUser().GetAwaiter().GetResult();
        }
        private void InitializeCategories()
        {
            if (!_context.Categories.Any())
            {
                var categories = new List<CategoriesModel>
                {
                    new CategoriesModel { name = "Eletrônicos", imageUrl = "https://picsum.photos/400/500?random=100" },
                    new CategoriesModel { name = "Celulares", imageUrl = "https://picsum.photos/400/500?random=101" },
                    new CategoriesModel { name = "Computadores", imageUrl = "https://picsum.photos/400/500?random=102" },
                    new CategoriesModel { name = "Games", imageUrl = "https://picsum.photos/400/500?random=103" },
                    new CategoriesModel { name = "TV e Áudio", imageUrl = "https://picsum.photos/400/500?random=104" },
                    new CategoriesModel { name = "Moda Masculina", imageUrl = "https://picsum.photos/400/500?random=105" },
                    new CategoriesModel { name = "Moda Feminina", imageUrl = "https://picsum.photos/400/500?random=106" },
                    new CategoriesModel { name = "Calçados", imageUrl = "https://picsum.photos/400/500?random=107" },
                    new CategoriesModel { name = "Acessórios", imageUrl = "https://picsum.photos/400/500?random=108" },
                    new CategoriesModel { name = "Móveis", imageUrl = "https://picsum.photos/400/500?random=109" },
                    new CategoriesModel { name = "Eletrodomésticos", imageUrl = "https://picsum.photos/400/500?random=110" },
                    new CategoriesModel { name = "Cama, Mesa e Banho", imageUrl = "https://picsum.photos/400/500?random=111" },
                    new CategoriesModel { name = "Decoração", imageUrl = "https://picsum.photos/400/500?random=112" },
                    new CategoriesModel { name = "Ferramentas", imageUrl = "https://picsum.photos/400/500?random=113" },
                    new CategoriesModel { name = "Materiais de Construção", imageUrl = "https://picsum.photos/400/500?random=114" },
                    new CategoriesModel { name = "Beleza e Cuidados Pessoais", imageUrl = "https://picsum.photos/400/500?random=115" },
                    new CategoriesModel { name = "Maquiagem", imageUrl = "https://picsum.photos/400/500?random=116" },
                    new CategoriesModel { name = "Esportes e Lazer", imageUrl = "https://picsum.photos/400/500?random=117" },
                    new CategoriesModel { name = "Camping e Aventura", imageUrl = "https://picsum.photos/400/500?random=118" },
                    new CategoriesModel { name = "Bebês e Crianças", imageUrl = "https://picsum.photos/400/500?random=119" },
                    new CategoriesModel { name = "Brinquedos", imageUrl = "https://picsum.photos/400/500?random=120" },
                    new CategoriesModel { name = "Livros", imageUrl = "https://picsum.photos/400/500?random=121" },
                    new CategoriesModel { name = "Papelaria", imageUrl = "https://picsum.photos/400/500?random=122" },
                    new CategoriesModel { name = "Pet Shop", imageUrl = "https://picsum.photos/400/500?random=123" },
                    new CategoriesModel { name = "Alimentos e Bebidas", imageUrl = "https://picsum.photos/400/500?random=124" },
                    new CategoriesModel { name = "Suplementos", imageUrl = "https://picsum.photos/400/500?random=125" }
                };

                _context.Categories.AddRange(categories);
                _context.SaveChanges();
            }
        }
        private async Task InitializeUser()
        {
            var roles = new[]
        {
            RoleHelper.GetRoleName(TypeUserRole.User),
            RoleHelper.GetRoleName(TypeUserRole.Admin),
            RoleHelper.GetRoleName(AccessRole.Standard),
            RoleHelper.GetRoleName(AccessRole.Seller)
        };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Criar usuário admin
            var adminUser = new UserModel
            {
                FirstName = "marcos",
                LastName = "oliveira",
                UserName = "marcosanca",
                Email = "marcos_sanca@example.com",
            };

            var resultAdmin = await _user.CreateAsync(adminUser, "Daniel@123");
            if (resultAdmin.Succeeded)
            {
                await _user.AddToRoleAsync(adminUser, RoleHelper.GetRoleName(TypeUserRole.Admin));
                await _user.AddClaimsAsync(adminUser, new[]
                {
                new Claim(ClaimTypeCustom.Email, adminUser.Email),
                new Claim(ClaimTypeCustom.PreferredUserName, adminUser.UserName)
            });

                var addressAdmin = new AddressCreateDto
                {
                    street = "Rua:Francisco Marigo 1388",
                    complement = "N/D",
                    neighborhood = "Jd. Cruzeiro do Sul",
                    city = "São Carlos",
                    state = "SP",
                    zipcode = "13572091",
                    cellPhone = "16993937320",
                    userId = adminUser.Id
                };
                _address.Create(addressAdmin);
            }

            // Criar usuário padrão
            var standardUser = new UserModel
            {
                FirstName = "david",
                LastName = "oliveira",
                UserName = "davidsanca",
                Email = "david_sanca@example.com",
            };

            var resultStandard = await _user.CreateAsync(standardUser, "Daniel@123");
            if (resultStandard.Succeeded)
            {
                await _user.AddToRoleAsync(standardUser, RoleHelper.GetRoleName(TypeUserRole.User));
                await _user.AddClaimsAsync(standardUser, new[]
                {
                new Claim(ClaimTypeCustom.Email, standardUser.Email),
                new Claim(ClaimTypeCustom.PreferredUserName, standardUser.UserName)
            });

                var addressStandard = new AddressCreateDto
                {
                    street = "Rua:Francisco Marigo 1388",
                    complement = "N/D",
                    neighborhood = "Jd. Cruzeiro do Sul",
                    city = "São Carlos",
                    state = "SP",
                    zipcode = "13572091",
                    cellPhone = "16993937320",
                    userId = standardUser.Id
                };
                _address.Create(addressStandard);
            }
        

        }
        
    }
}
