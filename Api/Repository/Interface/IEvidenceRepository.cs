using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IEvidenceRepository
    {
        Task<List<EvidenceModel>> GetAllEvidenceByProductId(Guid idProduct);
        Task<List<EvidenceModel>> GetAllEvidenceByShopId(Guid idShop);
    }
}
