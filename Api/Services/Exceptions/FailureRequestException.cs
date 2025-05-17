namespace ApiEstoque.Services.Exceptions
{
    public class FailureRequestException:Exception
    {
        public int StatusCode { get; }
        public string Mensagem { get; }

        public FailureRequestException(int statusCode, string mensagem) : base(mensagem)
        {
            StatusCode = statusCode;
            Mensagem = mensagem;
        }
    }
}
