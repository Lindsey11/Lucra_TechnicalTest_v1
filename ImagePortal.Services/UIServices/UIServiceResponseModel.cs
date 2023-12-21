using ImagePortal.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagePortal.Services.UIServices
{
    public class UIServiceResponseModel
    {
        public string messagae { get; set; } = string.Empty;
        public bool success { get; set; }
        public List<UIImageDataViewModel> data { get; set; }
    }
}
