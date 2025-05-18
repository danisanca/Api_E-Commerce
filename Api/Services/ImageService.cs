using ApiEstoque.Dto.Categories;
using ApiEstoque.Dto.Image;
using ApiEstoque.Helpers;
using ApiEstoque.Models;
using ApiEstoque.Repository;
using ApiEstoque.Repository.Base;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace ApiEstoque.Services
{
    public class ImageService : IImageService
    {
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;
        private readonly IBaseRepository<ProductModel> _productRepository;
        private readonly IBaseRepository<ImageModel> _baseRepository;

        public ImageService(IMapper mapper, IImageRepository imageRepository, IBaseRepository<ProductModel> productRepository, IBaseRepository<ImageModel> baseRepository)
        {
            _mapper = mapper;
            _imageRepository = imageRepository;
            _productRepository = productRepository;
            _baseRepository = baseRepository;
        }

        public async Task<bool> ActiveImage(int id)
        {
            
                try
                {
                    ImageModel image = await _baseRepository.SelectByIdAsync(id);
                    if (image == null) throw new FailureRequestException(404, "Id da imagem não localizado.");
                    if (image.status == StandartStatus.Ativo.ToString()) throw new FailureRequestException(409, "Imagem ja esta ativa.");
                    image.status = StandartStatus.Ativo.ToString();
                    return await _baseRepository.UpdateAsync(image);

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

        public async Task<ImageDto> CreateImage(ImageCreateDto image)
        {
            try
            {
                ImageModel getByUrl = await _imageRepository.GetImageByUrl(image.url);
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

        public async Task<bool> DisableImage(int id)
        {
            try
            {
                ImageModel image = await _baseRepository.SelectByIdAsync(id);
                if (image == null) throw new FailureRequestException(404, "Id da imagem não localizado.");
                if (image.status == StandartStatus.Desabilitado.ToString()) throw new FailureRequestException(409, "Imagem ja esta desabilitada.");
                image.status = StandartStatus.Desabilitado.ToString();
                return await _baseRepository.UpdateAsync(image);
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

        public async Task<List<ImageDto>> GetAllImages(FilterGetRoutes status = FilterGetRoutes.Ativo)
        {
            try
            {
                var findImages = await _baseRepository.SelectAllByStatusAsync(status);
                if (findImages == null) return new List<ImageDto>();
                return _mapper.Map<List<ImageDto>>(findImages);
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

        public async Task<ImageDto> GetImageById(int id)
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

        public async Task<List<ImageDto>> GetImagesByIdProduct(int idProduct)
        {
            try
            {
                var findProduct = await _productRepository.SelectByIdAsync(idProduct);
                if (findProduct == null) throw new FailureRequestException(404, "Id do produto nao localizada");
                var findImages = await _imageRepository.GetImagesByIdProduct(idProduct);
                return _mapper.Map<List<ImageDto>>(findImages);
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

        public async Task<ImageDto> GetImageByUrl(string url)
        {
            try
            {
                var findImage = await _imageRepository.GetImageByUrl(url);
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

        public async Task<bool> UpdateImage(ImageUpdateDto image)
        {
            try
            {
                var findImage = await _baseRepository.SelectByIdAsync(image.idImage);
                if (findImage == null) throw new FailureRequestException(404, "Id da imagem nao localizada");
                var findUrl = await _imageRepository.GetImageByUrl(image.url);
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
    }
}
