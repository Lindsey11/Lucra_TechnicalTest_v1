using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagePortal.Services.ViewModels
{
    public class UIImageMetaDataViewModel
    {
        public int imageMetaDataId { get; set; }

        public string? tags { get; set; }

        public string? categories { get; set; }
    }
}
