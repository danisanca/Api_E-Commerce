using ApiEstoque.Dto.Evidence;
using ApiEstoque.Models;

namespace ApiEstoque.Services.Interface
{
    public interface IEvidenceService
    {
        Task<EvidenceDto> CreateEvidence(EvidenceCreateDto model);
        Task<EvidenceDto> GetEvidenceById(int id);
        Task<List<EvidenceDto>> GetAllEvidenceByProductId(int idProduct);
        Task<List<EvidenceDto>> GetAllEvidenceByShopId(int idShop);
        Task<bool> DeleteEvidenceById(int idEvidence);
    }
}
