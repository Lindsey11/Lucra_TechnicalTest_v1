using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagePortal.Services.ViewModels
{
    public class ImageMetaDataViewModel
    {
        public int ImageMetaDataId { get; set; }

        public string? Tags { get; set; }

        public string? Categories { get; set; }
    }
}
