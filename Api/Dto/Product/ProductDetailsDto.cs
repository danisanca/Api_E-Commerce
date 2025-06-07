using ApiEstoque.Dto.Adress;
using ApiEstoque.Dto.Discount;
using ApiEstoque.Dto.Stock;

namespace ApiEstoque.Dto.Product
{
    public class ProductDetailsDto
    {
        public Guid Id { get; set; }
        public Guid ShopId { get; set; }
        public string NameShop { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Categoria { get; set; }
        public List<string> UrlImages { get; set; }
        public string Description { get; set; }
        public bool IsNew { get; set; }
        public StockDto? Stock { get; set; }
        public PercentDiscountDto? Discount { get; set; }
    }
    public class PercentDiscountDto
    {
        public float Value { get; set; }
    }
}
