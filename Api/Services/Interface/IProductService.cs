using ApiEstoque.Dto.Product;

namespace ApiEstoque.Services.Interface
{
    public interface IProductService
    {
        //Serviços Publicos
        Task<List<ProductDetailsDto>> GetAllWithDetails();
        Task<List<ProductDetailsDto>> GetAllWithDetailsLikeName(string name);

        //Serviços do Propietario da loja
        Task<ProductDto> Create(ProductCreateDto productModel);
        Task<bool> Update(ProductUpdateDto productModel);
        Task<bool> ChangeStatus(Guid idProduct,bool isActive);
        Task<List<ProductDetailsDto>> GetAllWithDetailsByIdShop(Guid idShop);

        //Somente em Serviços
        Task<ProductDto> GetById(Guid id);
    }
}
