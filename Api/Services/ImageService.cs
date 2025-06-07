using ApiEstoque.Constants;
using ApiEstoque.Dto.Categories;
using ApiEstoque.Dto.Image;
using ApiEstoque.Models;
using ApiEstoque.Repository;
using ApiEstoque.Repository.Base;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using static System.Net.Mime.MediaTypeNames;

namespace ApiEstoque.Services
{
    public class ImageService : IImageService
    {
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;
        private readonly IProductService _productService;
        private readonly IShopService _shopService;
        private readonly IBaseRepository<ImageModel> _baseRepository;

        public ImageService(IMapper mapper, IImageRepository imageRepository,
           IProductService productService, IBaseRepository<ImageModel> baseRepository, IShopService shopService)
        {
            _mapper = mapper;
            _imageRepository = imageRepository;
            _productService = productService;
            _baseRepository = baseRepository;
            _shopService = shopService;
        }


        public async Task<ImageDto> Create(ImageCreateDto image)
        {
            try
            {
                var findProduct = await _productService.GetById(image.productId);
                if (findProduct == null) throw new FailureRequestException(404, "Id do produto nao localizado.");

                var findShop = await _shopService.GetById(image.shopId);
                if (findShop == null) throw new FailureRequestException(404, "Id da loja nao localizada.");

                ImageModel getByUrl = await _imageRepository.GetByUrl(image.url);
                if (getByUrl != null) throw new FailureRequestException(409, "Url da imagem ja cadastrada.");

                var model = _mapper.Map<ImageModel>(image);
                model.status = StandartStatus.Ativo.ToString();
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
        public async Task<bool> Update(ImageUpdateDto image)
        {
            try
            {
                var findImage = await _baseRepository.SelectByIdAsync(image.idImage);
                if (findImage == null) throw new FailureRequestException(404, "Id da imagem nao localizada");
                var findUrl = await _imageRepository.GetByUrl(image.url);
                if (findUrl != null) throw new FailureRequestException(404, "Url ja cadastrada");
                var model = _mapper.Map<ImageModel>(findImage);
                model.url = image.url;
                return await _baseRepository.UpdateAsync(model);
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
                var findProduct = await _productService.GetById(idProduct);
                if (findProduct == null) throw new FailureRequestException(404, "Id do produto nao localizada");
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
    }
}
