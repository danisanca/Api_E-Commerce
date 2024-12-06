using ApiEstoque.Dto.Stock;
using ApiEstoque.Dto.User;
using ApiEstoque.Dto.ScoreProduct;
using ApiEstoque.Dto.Product;
using ApiEstoque.Dto.Image;
using ApiEstoque.Dto.HistoryPurchase;
using ApiEstoque.Dto.HistoryMoviment;
using ApiEstoque.Dto.Evidence;
using ApiEstoque.Dto.Categories;
using ApiEstoque.Models;
using AutoMapper;
using ApiEstoque.Dto.Shop;

namespace ApiEstoque.Data.Mapping.Dtos
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            #region User
            CreateMap<UserModel, UserDto>()
                    .ReverseMap();
            CreateMap<UserModel, UserCreateDto>()
                    .ReverseMap();
            CreateMap<UserModel, UserUpdateDto>()
                   .ReverseMap();
            #endregion

            #region Shop
            CreateMap<ShopModel, ShopDto>()
                    .ReverseMap();
            CreateMap<ShopModel, ShopCreateDto>()
                    .ReverseMap();
            CreateMap<ShopModel, ShopUpdateDto>()
                   .ReverseMap();
            #endregion

            #region Stock
            CreateMap<StockModel, StockDto>()
                    .ReverseMap();
            CreateMap<StockModel, StockCreateDto>()
                    .ReverseMap();
            CreateMap<StockModel, StockUpdateDto>()
                   .ReverseMap();
            #endregion

            #region ScoreProduct
            CreateMap<ScoreProductModel, ScoreProductDto>()
                    .ReverseMap();
            CreateMap<ScoreProductModel, ScoreProductCreateDto>()
                    .ReverseMap();
            #endregion

            #region Product
            CreateMap<ProductModel, ProductDto>()
                    .ReverseMap();
            CreateMap<ProductModel, ProductCreateDto>()
                    .ReverseMap();
            CreateMap<ProductModel, ProductUpdateDto>()
                   .ReverseMap();
            #endregion

            #region Image
            CreateMap<ImageModel, ImageDto>()
                    .ReverseMap();
            CreateMap<ImageModel, ImageCreateDto>()
                    .ReverseMap();
            CreateMap<ImageModel, ImageUpdateDto>()
                   .ReverseMap();
            #endregion

            #region HistoryPurchase
            CreateMap<HistoryPurchaseModel, HistoryPurchaseDto>()
                    .ReverseMap();
            CreateMap<HistoryPurchaseModel, HistoryPurchaseCreateDto>()
                    .ReverseMap();
            #endregion

            #region HistoryMoviment
            CreateMap<HistoryMovimentModel, HistoryMovimentDto>()
                    .ReverseMap();
            CreateMap<HistoryMovimentModel, HistoryMovimentCreateDto>()
                    .ReverseMap();
            #endregion

            #region Evidence
            CreateMap<EvidenceModel, EvidenceDto>()
                    .ReverseMap();
            CreateMap<EvidenceModel, EvidenceCreateDto>()
                    .ReverseMap();
            #endregion

            #region Categories
            CreateMap<CategoriesModel, CategoriesDto>()
                    .ReverseMap();
            CreateMap<CategoriesModel, CategoriesCreateDto>()
                    .ReverseMap();
            CreateMap<CategoriesModel, CategoriesUpdateDto>()
                    .ReverseMap();
            #endregion
        }
    }
}
