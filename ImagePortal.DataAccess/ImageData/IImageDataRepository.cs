using ImagePortal.DataContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagePortal.DataAccess.ImageData
{
    public interface IImageDataRepository
    {
        Task<ImageDatum> CreateAsync(ImageDatum imageDatum);
        Task CreateMetaData(ImageMetaDatum imageMetaDatum);
        Task<List<ImageDatum>> ReadAllImages();
        Task<ImageDatum?> ReadSingleAsync(int imageId);
        Task<bool> UpdateImageAsync(ImageDatum imageDatum);
        Task<bool> DeleteAsync(int imageId);
    }
}
