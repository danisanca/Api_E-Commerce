namespace ApiEstoque.Dto.Stock
{
    public class StockChangeStatusDto
    {
        public Guid idStock {  get; set; }
        public string userId { get; set; }
        public bool isActive { get; set; }
    }
}
