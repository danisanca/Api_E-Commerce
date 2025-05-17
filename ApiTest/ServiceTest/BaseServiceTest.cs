using ApiEstoque.Data.Mapping.Dtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiEstoqueTests.ServiceTest
{
    public abstract class BaseServiceTest
    {
        public IMapper Mapper { get; set; }
        public BaseServiceTest() {
            Mapper = new AutoMapperFixture().GetMapper();
        }

        public class AutoMapperFixture : IDisposable
        {
            public IMapper GetMapper()
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new DtoToModelProfile());
                });
                return config.CreateMapper();
            }
            public void Dispose()
            {

            }
        }
    }
}
