using ApiEstoque.Models.Base;

namespace ApiEstoque.Models
{
    public class ImageModel : BaseEntity
    {
        public string url { get; set; }
        public float size { get; set; }
        public Guid shopId { get; set; }
        public Guid productId { get; set; }
        public virtual ShopModel Shop { get; set; }
        public virtual ProductModel Product { get; set; }
    }
}
