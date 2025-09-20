namespace CartAPI.Dto
{
    public class CartUpdateDto
    {
        public Guid CartHeaderId { get; set; } = new Guid();
        public string UserId { get; set; }
        public IEnumerable<ItemCartDto> itemCarts { get; set; }

    }
   
}
