using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagePortal.Services.ViewModels
{
    public class ImageUploadModel
    {
        public int ImageId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public string Categories { get; set; }
        public string FileType { get; set; }
        public byte[] ImageData { get; set; }
    }

    public class ImageTags
    {
        public string TagName { get; set; }
    }

    public class ImageCategory
    {
        public string CategoryName { get; set; }
    }
}
