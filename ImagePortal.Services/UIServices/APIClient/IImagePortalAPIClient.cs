using ImagePortal.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagePortal.Services.UIServices.APIClient
{
    public interface IImagePortalAPIClient
    {
        Task<UIServiceResponseModel<List<UIImageDataViewModel>>> GetImages(int pageNumber, int pageSize);
        Task<bool> UploadImage(ImageUploadModel upload);
        Task<UIServiceResponseModel<UIImageDataViewModel>> GetImage(int deviceId);
        Task<bool> UpdateImageData(UIImageDataViewModel upload);
        Task<bool> DeleteImnage(int imageId);
    }
}
