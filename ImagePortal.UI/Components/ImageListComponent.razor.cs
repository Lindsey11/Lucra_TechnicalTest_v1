using ImagePortal.Services.UIServices.APIClient;
using ImagePortal.Services.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Net.WebSockets;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ImagePortal.UI.Components
{
    public partial class ImageListComponent
    {
        [Inject]
        private IImagePortalAPIClient _apiClient { get; set; }
        public List<UIImageDataViewModel> Images { get; set; } = new List<UIImageDataViewModel>();
        private CustomDialog dialog { get; set; }
        public bool IsLoadding { get; set; }
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 8;
        protected override async Task OnInitializedAsync()
        {
            await GetImages();
        }

        public async Task GetImages()
        {
            try
            {
                IsLoadding = true;
                var data = await _apiClient.GetImages(pageNumber, pageSize);

                if (data.success)
                {
                    Images = data.data;
                }
                else
                {
                    dialog.Show("Error", $"Error {data.messagae}", false);
                }

                IsLoadding = false;
            }
            catch (Exception error)
            {
                IsLoadding = false;
                dialog.Show("Error", $"Error {error.Message}", false);
            }
        }

        public async Task LoadNextBatch()
        {
            pageNumber++;
            IsLoadding = true;
                Images = new List<UIImageDataViewModel>();
                await GetImages();
            IsLoadding = false;
        }

        public async Task LoadPreviousBatch()
        {

            pageNumber--;
            IsLoadding = true;
            Images = new List<UIImageDataViewModel>();
            await GetImages();
            IsLoadding = false;
        }
    }
}
