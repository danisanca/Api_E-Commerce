using AutoMapper;
using OrderAPI.Dtos;
using OrderAPI.Models;
using OrderAPI.Repository.Interface;
using OrderAPI.Services.Interface;
using SharedBase.Dtos.Cart;
using SharedBase.Helpers.Exceptions;

namespace OrderAPI.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderHeaderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderServices(IOrderHeaderRepository orderRepository, IMapper mapper)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<bool> Create(OrderHeader header)
        {
            try
            {
               var result = await _orderRepository.Create(header);

                if(result == null)
                    throw new FailureRequestException(409, "Não foi possivel criar o pedido de compra");

                return true;
            
            }
            catch (FailureRequestException ex)
            {

                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<OrderHeaderDto> GetHeaderById(Guid id)
        {
            try
            {
                var result = await _orderRepository.GetById(id);
                if (result == null)
                    return null;
                var orderHeader = _mapper.Map<OrderHeaderDto>(result);
                var orderDetails = _mapper.Map<List<OrderDetailsDto>>(result.OrderDetails);
                orderHeader.OrderDetailsDto = orderDetails;
                

                return orderHeader ;

            }
            catch (FailureRequestException ex)
            {

                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateHeader(OrderHeader header)
        {
            try
            {
                var result = await _orderRepository.Update(header);

                if (result == null)
                    throw new FailureRequestException(409, "Falha em atualizar o pedido");

                return true;
            }
            catch (FailureRequestException ex)
            {

                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
