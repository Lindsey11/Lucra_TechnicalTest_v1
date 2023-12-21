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
        Task<UIServiceResponseModel> GetImages(int pageNumber, int pageSize);
        Task<bool> UploadImage(ImageUploadModel upload);
    }
}
