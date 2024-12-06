using ApiEstoque.Dto.Categories;
using ApiEstoque.Dto.Image;
using ApiEstoque.Helpers;
using ApiEstoque.Models;
using ApiEstoque.Repository;
using ApiEstoque.Repository.Interface;
using ApiEstoque.Services.Exceptions;
using ApiEstoque.Services.Interface;
using AutoMapper;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace ApiEstoque.Services
{
    public class ImageService : IImageService
    {
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;

        public ImageService(IMapper mapper, IImageRepository imageRepository)
        {
            _mapper = mapper;
            _imageRepository = imageRepository;
        }

        public async Task<bool> ActiveImage(int id)
        {
            
                try
                {
                    ImageModel image = await _imageRepository.GetImageById(id);
                    if (image == null) throw new FailureRequestException(404, "Id da imagem não localizado.");
                    if (image.status == StandartStatus.Ativo.ToString()) throw new FailureRequestException(409, "Imagem ja esta ativa.");
                image.status = StandartStatus.Ativo.ToString();
                await _imageRepository.UpdateImage(image);
                return true;

            }
                catch (FailureRequestException ex)
                {
                    throw new FailureRequestException(ex.StatusCode, ex.Message);
                }
                catch (Exception e)
                {
                    throw new Exception($"Falha ao ativar categoria. Detalhe do erro: {e.Message}");
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
                await _imageRepository.CreateImage(model);
                return _mapper.Map<ImageDto>(model);
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception($"Falha ao criar image. Detalhe do erro: {e.Message}");
            }
        }

        public async Task<bool> DisableImage(int id)
        {
            try
            {
                ImageModel image = await _imageRepository.GetImageById(id);
                if (image == null) throw new FailureRequestException(404, "Id da imagem não localizado.");
                if (image.status == StandartStatus.Desabilitado.ToString()) throw new FailureRequestException(409, "Imagem ja esta desabilitada.");
                image.status = StandartStatus.Desabilitado.ToString();
                await _imageRepository.UpdateImage(image);
                return true;
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception($"Falha ao criar image. Detalhe do erro: {e.Message}");
            }
        }

        public async Task<List<ImageDto>> GetAllImages(FilterGetRoutes status = FilterGetRoutes.All)
        {
            try
            {
                var findImages = await _imageRepository.GetAllImages(status);
                if (findImages == null) throw new FailureRequestException(200, "Nenhuma imagem cadastrada");
                return _mapper.Map<List<ImageDto>>(findImages);
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception($"Falha ao criar image. Detalhe do erro: {e.Message}");
            }
        }

        public async Task<ImageDto> GetImageById(int id)
        {
            try
            {
                var findImage = await _imageRepository.GetImageById(id);
                if (findImage == null) throw new FailureRequestException(404, "Id da imagem nao localizada");
                return _mapper.Map<ImageDto>(findImage);
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception($"Falha ao criar image. Detalhe do erro: {e.Message}");
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
                throw new Exception($"Falha ao criar image. Detalhe do erro: {e.Message}");
            }
        }

        public async Task<bool> UpdateImage(ImageUpdateDto image)
        {
            try
            {
                var findImage = await _imageRepository.GetImageById(image.idImage);
                if (findImage == null) throw new FailureRequestException(404, "Id da imagem nao localizada");
                var findUrl = await _imageRepository.GetImageByUrl(image.url);
                if (findUrl != null) throw new FailureRequestException(404, "Url ja cadastrada");
                var model = _mapper.Map<ImageModel>(findImage);
                model.url = image.url;
                await _imageRepository.UpdateImage(model);
                return true;
            }
            catch (FailureRequestException ex)
            {
                throw new FailureRequestException(ex.StatusCode, ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception($"Falha ao criar image. Detalhe do erro: {e.Message}");
            }
        }
    }
}
