using System.Text.Json.Serialization;

namespace ApiEstoque.Constants
{

    public enum StandartStatus
    {
        Ativo,
        Desabilitado
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FilterGetRoutes
    {
        All,
        Ativo,
        Desabilitado
    }
    public enum MovimentAction
    {
        Entrada,
        Saida,
        Venda,
        Devolução,
        Acerto,
        Bloqueio,
        Desbloqueio
    }

}
