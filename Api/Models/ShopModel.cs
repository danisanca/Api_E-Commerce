using ApiEstoque.Constants;
using SharedBase.Models;

namespace ApiEstoque.Models
{
    public class ShopModel : BaseEntity
    {
        public string name { get; set; }
        public string userId { get; set; }
        public virtual UserModel user { get; set; }

        public IEnumerable<ProductModel> products { get; set; }
        public IEnumerable<StockModel> stock { get; set; }
        public string status { get; set; } = FilterGetRoutes.Ativo.ToString();

    }
}
