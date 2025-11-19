using ApiEstoque.Constants;
using ApiEstoque.Dto.Product;

namespace ApiEstoque.Services.Interface
{
    public interface IProductService
    {
        //Serviços Publicos
        Task<ProductViewModel> GetAllWithDetails(int limit = 20, int page = 0, string category = "");
        Task<ProductViewModel> GetAllWithDetailsLikeName(string name);
        Task<ProductDetailsDto> GetWithDetailsById(Guid id);

        //Serviços do Propietario da loja
        Task<ProductDto> Create(ProductCreateDto productModel);
        Task<bool> Update(ProductUpdateDto productModel);
        Task<bool> ChangeStatus(Guid idProduct,bool isActive);
        Task<ProductViewModel> GetAllWithDetailsByIdShop(Guid idShop, FilterGetRoutes status = FilterGetRoutes.All,int limit = 20, int page = 0, string category = "");

        //Somente em Serviços
        Task<ProductDto> GetById(Guid id);
    }
}
