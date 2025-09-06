namespace CartAPI.Dto
{
    public class CartDetailDto
    {
        public Guid Id { get; set; }
        public Guid CartHeaderId { get; set; }
        public int Count { get; set; }
        public Guid ProductId { get; set; }
       
    }
}
