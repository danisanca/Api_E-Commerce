using System.Security.Policy;
using ApiEstoque.Constants;
using ApiEstoque.Dto.HistoryMoviment;
using ApiEstoque.Models;
using ApiEstoque.Repository;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SharedBase.Repository;

namespace ApiEstoque.Services
{
    public class HistoryMovimentService : IHistoryMovimentService
    {
        private readonly IMapper _mapper;
        private readonly IHistoryMovimentRepository _historyMovimentRepository;
        private readonly UserManager<UserModel> _userManager;
        private readonly IBaseRepository<HistoryMovimentModel> _baseRepository;

        public HistoryMovimentService (IMapper mapper, 
            IHistoryMovimentRepository historyMovimentRepository,
            UserManager<UserModel> userManager,
            IBaseRepository<HistoryMovimentModel> baseRepository
            )
        {
            _mapper = mapper;
            _userManager = userManager;
            _baseRepository = baseRepository;
            _historyMovimentRepository = historyMovimentRepository;
        }

        public async Task<HistoryMovimentDto> Create(HistoryMovimentCreateDto model)
        {
            try
            {
                var findUser = await _userManager.FindByIdAsync(model.userId.ToString());
                if (findUser == null) throw new FailureRequestException(404, "Id do usuario nao localizado");
                if (model.action != MovimentAction.Entrada.ToString() &&
                    model.action != MovimentAction.Saida.ToString() && 
                    model.action != MovimentAction.Venda.ToString() && 
                    model.action != MovimentAction.Devolução.ToString() &&
                    model.action != MovimentAction.Acerto.ToString() &&
                    model.action != MovimentAction.Bloqueio.ToString() &&
                    model.action != MovimentAction.Desbloqueio.ToString()) 
                    throw new FailureRequestException(404, "Tipo de Movimentação Invalida.");
                var history = _mapper.Map<HistoryMovimentModel>(model);
                
                return _mapper.Map<HistoryMovimentDto>(await _baseRepository.InsertAsync(history));
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<HistoryMovimentDto>> GetAllByProductId(Guid idProduct)
        {
            
            try
            {
               var findHistory = await _historyMovimentRepository.GetAllHistoryMovimentByProductId(idProduct);
                if (findHistory == null) return new List<HistoryMovimentDto>();
                return _mapper.Map<List<HistoryMovimentDto>>(findHistory);
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<HistoryMovimentDto> GetById(Guid id)
        {
           
            try
            {
                var history = await _baseRepository.SelectByIdAsync(id);
                if (history == null) throw new FailureRequestException(404, "Id do historico não localizado.");
                return _mapper.Map<HistoryMovimentDto>(history);
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
