using ApiEstoque.Models;

namespace ApiEstoque.Repository.Interface
{
    public interface IEvidenceRepository
    {
        Task<EvidenceModel> addEvidence(EvidenceModel model);
        Task<EvidenceModel> GetEvidenceById(int id);
        Task<List<EvidenceModel>> GetAllEvidenceByProductId(int idProduct);
        Task<List<EvidenceModel>> GetAllEvidenceByShopId(int idShop);
        Task<bool> deleteEvidence(EvidenceModel model);
    }
}
