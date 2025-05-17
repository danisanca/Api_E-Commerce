using ApiEstoque.Data;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstoqueTests.RepositoryTest
{
    public abstract class DbBaseTest
    {
        public DbBaseTest() { }
    }
    public class DbTest : IDisposable
    {
        private string dataBaseName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";

        public ServiceProvider ServiceProvider { get; private set; }

        public DbTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<ApiContext>(options =>
            options.UseSqlServer($"Server=localhost;Initial Catalog={dataBaseName};User Id=sa;Password=Dani@123456;TrustServerCertificate=True"),
            ServiceLifetime.Transient
            );
            
            ServiceProvider = serviceCollection.BuildServiceProvider();

            using (var context = ServiceProvider.GetService<ApiContext>())
            {
                context.Database.EnsureCreated();
            }
        }

        public void Dispose()
        {
            using (var context = ServiceProvider.GetService<ApiContext>())
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}
