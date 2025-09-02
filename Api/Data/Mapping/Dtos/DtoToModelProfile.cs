using ApiEstoque.Dto.Stock;
using ApiEstoque.Dto.User;
using ApiEstoque.Dto.Product;
using ApiEstoque.Dto.Image;
using ApiEstoque.Dto.HistoryMoviment;
using ApiEstoque.Dto.Categories;
using ApiEstoque.Models;
using AutoMapper;
using ApiEstoque.Dto.Shop;
using ApiEstoque.Dto.Discount;
using ApiEstoque.Dto.Adress;
using System.Text.Json;
using ApiEstoque.Dto.Address;

namespace ApiEstoque.Data.Mapping.Dtos
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            #region Address
            CreateMap<AddressModel, AddressCreateDto>()
                    .ReverseMap();
            CreateMap<AddressModel, AddressDto>()
                    .ReverseMap();
            CreateMap<AddressModel, AddressViewDto>()
                   .ReverseMap();
            CreateMap<AddressModel, AddressUpdateDto>()
                   .ReverseMap();
            #endregion

            #region User

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
            CreateMap<StockModel, StockUpdateDto>()
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
            #endregion

            #region HistoryMoviment
            CreateMap<HistoryMovimentModel, HistoryMovimentDto>()
                    .ReverseMap();
            CreateMap<HistoryMovimentModel, HistoryMovimentCreateDto>()
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

            #region Discount
            CreateMap<DiscountModel, DiscountCreateDto>()
                    .ReverseMap();
            CreateMap<DiscountModel, DiscountDto>()
                    .ReverseMap();
            CreateMap<DiscountModel, DiscountUpdateDto>()
                    .ReverseMap();
            #endregion
        }
    }
}
