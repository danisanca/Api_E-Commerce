using ApiEstoque.Constants;
using ApiEstoque.Dto.Categories;
using ApiEstoque.Dto.Image;
using ApiEstoque.Models;
using ApiEstoque.Repository;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using AutoMapper;
using SharedBase.Repository;

namespace ApiEstoque.Services
{
    public class ImageService : IImageService
    {
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;
        private readonly IBaseRepository<ImageModel> _baseRepository;

        public ImageService(IMapper mapper, IImageRepository imageRepository,
           IBaseRepository<ImageModel> baseRepository)
        {
            _mapper = mapper;
            _imageRepository = imageRepository;
            _baseRepository = baseRepository;
        }

        
        public async Task<ImageDto> Create(ImageCreateDto image)
        {// Todo: Implementar depois logica de salvamento da imagem no bucket
            try
            {
                //ImageModel getByUrl = await _imageRepository.GetByUrl(image.url);
                //if (getByUrl != null) throw new FailureRequestException(409, "Url da imagem ja cadastrada.");

                var model = new ImageModel()
                {
                    url = image.productId.ToString(), // ajustar Posteriormente
                    size = 1f,
                    shopId = image.shopId,
                    productId = image.productId

                };
                return _mapper.Map<ImageDto>(await _baseRepository.InsertAsync(model));
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
        public async Task<bool> Delete(Guid id)
        {
            try
            {
                ImageModel result = await _baseRepository.SelectByIdAsync(id);
                if (result == null) throw new FailureRequestException(404, "Id da imagem não localizado.");
                return await _baseRepository.DeleteAsync(id);
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

        public async Task<ImageDto> GetById(Guid id)
        {
            try
            {
                var findImage = await _baseRepository.SelectByIdAsync(id);
                if (findImage == null) throw new FailureRequestException(404, "Id da imagem nao localizada");
                return _mapper.Map<ImageDto>(findImage);
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

        public async Task<List<string>> GetAllByIdProduct(Guid idProduct)
        {
            try
            {
                var findImages = await _imageRepository.GetAllByIdProduct(idProduct);
                var listUrl = new List<string>();
                foreach (var image in findImages)
                {
                    listUrl.Add(image.url);
                }
                return listUrl;
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

        public async Task<ImageDto> GetByUrl(string url)
        {
            try
            {
                var findImage = await _imageRepository.GetByUrl(url);
                if (findImage == null) throw new FailureRequestException(404, "Id da imagem nao localizada");
                return _mapper.Map<ImageDto>(findImage);
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

        public async Task<List<ImageDto>> GetAllByProductsIds(List<Guid> ids)
        {
            try
            {
                var findImage = await _imageRepository.GetAllByProductsIds(ids);
                if (findImage == null) throw new FailureRequestException(404, "nenhuma imagen encontrada para a lista de ids");
                return _mapper.Map<List<ImageDto>>(findImage);
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

        public async Task<bool> DeleteByUrl(string url)
        {
            try
            {
                var result = await _imageRepository.GetByUrl(url);
                if (result == null) throw new FailureRequestException(404, "Id da imagem não localizado.");
                return await _baseRepository.DeleteAsync(result.Id);
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
