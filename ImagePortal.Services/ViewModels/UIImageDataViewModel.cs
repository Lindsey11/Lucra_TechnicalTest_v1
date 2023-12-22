using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagePortal.Services.ViewModels
{
    public class UIImageDataViewModel
    {
        public int imageId { get; set; }

        public string title { get; set; }

        public string description { get; set; }
        public bool hasMetaData { get; set; }
        public UIImageMetaDataViewModel? imageMetaDataViewModel { get; set; }
        public byte[]? imageData { get; set; }
        public string fileType { get; set; } 
        public string imageUrl { get; set; }
    }
}
