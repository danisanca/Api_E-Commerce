using AutoMapper;
using OrderAPI.Dtos;
using OrderAPI.Models;

namespace OrderAPI.Data.Mapping.Dto
{
    public class DtoToModel : Profile
    {
        public DtoToModel()
        {

            #region OrderHeader
            CreateMap<OrderHeader, OrderHeaderDto>()
                    .ReverseMap();
            #endregion

            #region OrderDetail
            CreateMap<OrderDetail, OrderDetailsDto>()
                    .ReverseMap();
            #endregion
        }
    }
}
