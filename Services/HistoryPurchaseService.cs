using ApiEstoque.Dto.HistoryMoviment;
using ApiEstoque.Dto.HistoryPurchase;
using ApiEstoque.Helpers;
using ApiEstoque.Models;
using ApiEstoque.Repository;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using AutoMapper;

namespace ApiEstoque.Services
{
    public class HistoryPurchaseService : IHistoryPurchaseService
    {
        private readonly IMapper _mapper;
        private readonly IHistoryPurchaseRepository _historyPurchaseRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IImageRepository _imageRepository;

        public HistoryPurchaseService(IMapper mapper, IHistoryPurchaseRepository historyPurchaseRepository,
            IShopRepository shopRepository, IProductRepository productRepository, IUserRepository userRepository, IImageRepository imageRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _historyPurchaseRepository = historyPurchaseRepository;
            _shopRepository = shopRepository;
            _productRepository = productRepository;
            _imageRepository = imageRepository;
        }

        public async Task<HistoryPurchaseDto> CreateHistoryPurchase(HistoryPurchaseCreateDto model)
        {
            try
            {
                var findProduct = await _productRepository.GetProductById(model.productId);
                if (findProduct == null) throw new FailureRequestException(404, "Id do produto nao localizado");
                var findUser = await _userRepository.GetUserById(model.userId);
                if (findUser == null) throw new FailureRequestException(404, "Id do usuario nao localizado");
                var history = _mapper.Map<HistoryPurchaseModel>(model);
                
                return _mapper.Map<HistoryPurchaseDto>(await _historyPurchaseRepository.AddHistory(history));
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

        public async Task<List<HistoryPurchaseDto>> GetAllHistoryPurchaseByProductId(int idProduct)
        {
            
            try
            {
                var findProduct = await _productRepository.GetProductById(idProduct);
                if (findProduct == null) throw new FailureRequestException(404, "Id do produto nao localizado");
                var findHistory = await _historyPurchaseRepository.GetAllHistoryPurchaseByProductId(idProduct);
                if (findHistory == null) return new List<HistoryPurchaseDto>();
                return _mapper.Map<List<HistoryPurchaseDto>>(findHistory);
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

        public async Task<List<HistoryPurchaseDto>> GetAllHistoryPurchaseByShopId(int idShop)
        {
            
            try
            {
                var findShop = await _shopRepository.GetShopById(idShop);
                if (findShop == null) throw new FailureRequestException(404, "Id do shop nao localizado");
                var findHistory = await _historyPurchaseRepository.GetAllHistoryPurchaseByShopId(idShop);
                if (findHistory == null) return new List<HistoryPurchaseDto>();
                return _mapper.Map<List<HistoryPurchaseDto>>(findHistory);
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

        public async Task<List<HistoryPurchaseFullDto>> GetAllHistoryPurchaseByUserId(int idUser)
        {
            var findUser = await _userRepository.GetUserById(idUser);
            if (findUser == null) throw new FailureRequestException(404, "Id do usuario nao localizado");
            var findHistory = await _historyPurchaseRepository.GetAllHistoryPurchaseByUserId(idUser);

            var historyList = new List<HistoryPurchaseFullDto>();

            foreach (var history in findHistory)
            {
                var findProduct = await _productRepository.GetProductById(history.productId);
                var findImage = await _imageRepository.GetImagesByIdProduct(findProduct.id);
                var listUrls = findImage.Select(img => img.url).ToList();
                var model = new HistoryPurchaseFullDto
                {
                    id = history.id,
                    createdAt = history.createdAt,
                    item = new ItemDto
                    {
                        productId = history.productId,
                        productName = findProduct.description,
                        amount = history.amount,
                        imageUrl = listUrls,
                        description = findProduct.description,
                        priceProduct = history.price,
                        priceTotal = (history.price * history.amount),
                    }
                };

                historyList.Add(model);
            }
            return historyList;
        }

        public async Task<HistoryPurchaseDto> GetHistoryPurchaseById(int id)
        {
            
            try
            {
                var history = await _historyPurchaseRepository.GetHistoryPurchaseById(id);
                if (history == null) throw new FailureRequestException(404, "Id do historico não localizado.");
                return _mapper.Map<HistoryPurchaseDto>(history);
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
