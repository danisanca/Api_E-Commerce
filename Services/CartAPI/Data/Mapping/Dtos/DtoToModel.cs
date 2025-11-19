using AutoMapper;
using CartAPI.Dto;
using CartAPI.Models;
using SharedBase.Dtos.Cart;
namespace CartAPI.Data.Mapping.Dtos
{
    public class DtoToModel : Profile
    {
        public DtoToModel() {

            #region CartHeader
            CreateMap<CartHeader, CartHeaderDto>()
                    .ReverseMap();
            #endregion

            #region CartDetail
            CreateMap<CartDetail, CartDetailDto>()
                    .ReverseMap();
            #endregion
        }
    }
}
