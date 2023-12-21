using ImagePortal.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagePortal.Services.ApiServices
{
    public interface IImageDataService
    {
        Task<APIServiceResponseModel<List<ImageDataViewModel>>> GetAllImageData();
        Task<APIServiceResponseModel<List<ImageDataViewModel>>> GetAllImageDataPaginated(int pageNumber, int pageSize);
        Task<APIServiceResponseModel<ImageDataViewModel>> GetImageData(int imageId);
        Task<APIServiceResponseModel<bool>> CreateNewImage(ImageDataViewModel request);
        Task<APIServiceResponseModel<bool>> DeleteImage(int imageId);
        Task<APIServiceResponseModel<bool>> UpdateImage(ImageDataViewModel requst);
        Task<APIServiceResponseModel<bool>> UpdateMetaData(ImageMetaDataViewModel imageMetaDataViewModel);
    }
}
