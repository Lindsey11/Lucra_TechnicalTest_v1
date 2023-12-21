using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagePortal.Services.ApiServices
{
    public class APIServiceResponseModel<T>
    {
        public string Message { get; set; }
        public bool Success { get; set; } 
        public T? Data { get; set; }
    }
}
