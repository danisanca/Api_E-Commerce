using ApiEstoque.Dto.Evidence;
using ApiEstoque.Models;

namespace ApiEstoque.Services.Interface
{
    public interface IEvidenceService
    {
        Task<EvidenceDto> CreateEvidence(EvidenceCreateDto evidenceCreate);
        Task<EvidenceDto> GetEvidenceById(Guid id);
        Task<List<EvidenceDto>> GetAllEvidenceByProductId(Guid idProduct);
        Task<List<EvidenceDto>> GetAllEvidenceByShopId(Guid idShop);
        Task<bool> DeleteEvidenceById(Guid idEvidence);
    }
}
