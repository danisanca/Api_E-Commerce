using ApiEstoque.Models.Base;

namespace ApiEstoque.Models
{
    public class ImageModel : BaseEntity
    {
        public string url { get; set; }
        public float size { get; set; }
        public int shopId { get; set; }
        public int productId { get; set; }
        public virtual ShopModel Shop { get; set; }
    }
}
