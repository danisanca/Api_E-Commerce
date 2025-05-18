using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IEvidenceRepository
    {
        Task<List<EvidenceModel>> GetAllEvidenceByProductId(int idProduct);
        Task<List<EvidenceModel>> GetAllEvidenceByShopId(int idShop);
    }
}
