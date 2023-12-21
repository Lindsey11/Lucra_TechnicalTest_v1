using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagePortal.Services.ViewModels
{
    public class ImageDataViewModel
    {
        public int ImageId { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;
        public bool HasMetaData { get; set; }
        public ImageMetaDataViewModel? ImageMetaDataViewModel { get; set; }
        public byte[]? ImageData { get; set; }
        public string? ImageUrl { get; set; }
        public string FileType { get; set; } = null!;
        public IFormFile? file { get; set; }
      
    }
}
