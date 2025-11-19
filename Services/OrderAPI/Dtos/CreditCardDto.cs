namespace OrderAPI.Dtos
{
    public class CreditCardDto
    {
        public string NameCard { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string ExpiryMonthYear { get; set; }
    }
}
