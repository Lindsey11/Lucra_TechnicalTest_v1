using ImagePortal.Services.UIServices.APIClient;
using ImagePortal.Services.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace ImagePortal.UI.Components
{
    public partial class ImageListComponent
    {
        [Inject]
        private IImagePortalAPIClient _apiClient { get; set; }
        public List<UIImageDataViewModel> Images { get; set; } = new List<UIImageDataViewModel>();
        public bool IsLoadding { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await GetImages();
        }

        public async Task GetImages()
        {
            try
            {
                IsLoadding = true;
                var data = await _apiClient.GetImages(1, 10);

                Images = data.data;

                IsLoadding = false;
            }
            catch (Exception error)
            {
                Console.Write(error.Message);
            }
        }

       
    }
}
