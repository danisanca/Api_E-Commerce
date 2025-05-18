using ApiEstoque.Models.Base;

namespace ApiEstoque.Models
{
    public class ShopModel : BaseEntity
    {
        public string name { get; set; }
        public int userId { get; set; }
        public virtual UserModel user { get; set; }

        public IEnumerable<ProductModel> products { get; set; }

    }
}
