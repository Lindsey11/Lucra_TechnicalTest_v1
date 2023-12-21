using ImagePortal.DataAccess.ImageData;
using ImagePortal.DataContext.Models;
using ImagePortal.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagePortal.Services.ApiServices
{
    public class ImageDataService : IImageDataService
    {
        private readonly IImageDataRepository _imageRepo;

        public ImageDataService(IImageDataRepository imageDataRepository)
        {
            _imageRepo = imageDataRepository;
        }

        public async Task<APIServiceResponseModel<bool>> CreateNewImage(ImageDataViewModel request)
        {
            try
            {
                var newImage = new ImageDatum()
                {
                    Data = request.ImageData,
                    DateCreated = DateTime.Now,
                    Description = request.Description,
                    FileType = request.FileType,
                    Title = request.Title,
                    Base64URL = ConvertToBase64(request.ImageData,request.FileType)
                };

                var newImageId = await _imageRepo.CreateAsync(newImage);

                if (request.HasMetaData && newImageId is not null)
                {

                    var newMetaData = new ImageMetaDatum()
                    {
                        DateCreated = DateTime.Now,
                        Tags = request.ImageMetaDataViewModel.Tags, // This be converted to a json array on the UI before passing,
                        Categories = request.ImageMetaDataViewModel.Categories,
                        ImageId = newImageId.ImageId
                    };

                    await _imageRepo.CreateMetaData(newMetaData);
                }


                return new() { Message = "New image added", Success = true };
            }
            catch (Exception error)
            {
                return new() { Message = error.Message , Success = false };
            }
        }

        public async Task<APIServiceResponseModel<bool>> DeleteImage(int imageId)
        {
            try
            {
                var deletImageResult = await _imageRepo.DeleteAsync(imageId);

                if(!deletImageResult)
                    return new() { Message = "Error deleting Image", Success=false };

                return new() { Message = "Image Deleted", Success = true };

            }
            catch (Exception error)
            {
                return new() { Message = error.Message, Success = false };
            }
        }

        public async Task<APIServiceResponseModel<List<ImageDataViewModel>>> GetAllImageData()
        {
            try
            {
                var allImages = await _imageRepo.ReadAllImages();

                var res = new List<ImageDataViewModel>();

                foreach (var image in allImages)
                {
                    var imageData = new ImageDataViewModel()
                    {
                        Description = image.Description,
                        FileType = image.FileType,
                        ImageData = image.Data,
                        ImageId = image.ImageId,
                        Title = image.Title,
                        ImageUrl = image.Base64URL
                    };

                    if(image.ImageMetaData is not null)
                    {
                        imageData.HasMetaData = true;
                        imageData.ImageMetaDataViewModel = new()
                        {
                            Categories = image.ImageMetaData.FirstOrDefault()?.Categories,
                            Tags = image.ImageMetaData.FirstOrDefault()?.Tags,
                            ImageMetaDataId = image.ImageMetaData.FirstOrDefault()?.ImageMetaDataId ?? 0,
                        };
                    }

                    res.Add(imageData);
                }

                return new() { Data = res , Success = true, Message = "" };
            }
            catch (Exception error)
            {
                return new() { Message = error.Message, Success = false };
            }
        }
        public async Task<APIServiceResponseModel<List<ImageDataViewModel>>> GetAllImageDataPaginated(int pageNumber, int pageSize = 10)
        {
            try
            {
                var allImages = await _imageRepo.ReadAllImages();

                var count = allImages.Count;

                var result = allImages.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                var res = new List<ImageDataViewModel>();

                foreach (var image in result)
                {
                    var imageData = new ImageDataViewModel()
                    {
                        Description = image.Description,
                        FileType = image.FileType,
                        ImageData = image.Data,
                        ImageId = image.ImageId,
                        Title = image.Title,
                        ImageUrl = image.Base64URL
                    };

                    if (image.ImageMetaData is not null)
                    {
                        imageData.HasMetaData = true;
                        imageData.ImageMetaDataViewModel = new()
                        {
                            Categories = image.ImageMetaData.FirstOrDefault()?.Categories,
                            Tags = image.ImageMetaData.FirstOrDefault()?.Tags,
                            ImageMetaDataId = image.ImageMetaData.FirstOrDefault()?.ImageMetaDataId ?? 0,
                        };
                    }

                    res.Add(imageData);
                }

                return new() { Data = res, Success = true, Message = "" };

            }
            catch (Exception error)
            {
                return new() { Message = error.Message, Success = false };
            }
        }
        public async Task<APIServiceResponseModel<ImageDataViewModel>> GetImageData(int imageId)
        {
            try
            {
                var singleImage = await _imageRepo.ReadSingleAsync(imageId);

                if(singleImage is null)
                    return new() { Message = "No Image found", Success = false };

                var imageData = new ImageDataViewModel()
                {
                    Description = singleImage.Description,
                    FileType = singleImage.FileType,
                    ImageId = singleImage.ImageId,
                    ImageData = singleImage.Data,
                    Title = singleImage.Title,
                };

                if(singleImage.ImageMetaData is not null)
                {
                    imageData.HasMetaData = true;
                    imageData.ImageMetaDataViewModel = new ImageMetaDataViewModel()
                    {
                        Categories = singleImage.ImageMetaData.FirstOrDefault()?.Categories,
                        Tags = singleImage.ImageMetaData.FirstOrDefault()?.Tags,
                        ImageMetaDataId = singleImage.ImageMetaData.FirstOrDefault()?.ImageId ?? 0,
                    };
                }

                return new() { Data = imageData , Success = true, Message = "" };

            }
            catch (Exception error)
            {
                return new() { Message = error.Message, Success = false };
            }
        }

        public async Task<APIServiceResponseModel<bool>> UpdateImage(ImageDataViewModel requst)
        {
            try
            {
                var ImageTodata = new ImageDatum()
                {
                    Data = requst.ImageData,
                    Description = requst.Description,
                    FileType = requst.FileType,
                    ImageId = requst.ImageId,
                    Title = requst.Title
                };

                var updateResult = await _imageRepo.UpdateImageAsync(ImageTodata);

                if (!updateResult)
                    return new() { Success = false, Message = "Error updating Image" };


                return new() { Success = true, Message = "Image updated successfully" };


            }
            catch (Exception error)
            {
                return new() { Message = error.Message, Success = false };
            }
        }

        public Task<APIServiceResponseModel<bool>> UpdateMetaData(ImageMetaDataViewModel imageMetaDataViewModel)
        {
            throw new NotImplementedException();
        }

        private string ConvertToBase64(byte[] imageBytes, string fileType)
        {
            return $"data:image/{fileType};base64,{Convert.ToBase64String(imageBytes)}";
        }
    }
}
