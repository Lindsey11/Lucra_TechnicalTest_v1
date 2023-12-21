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
        public List<UIImageDataViewModel> Images { get; set; }
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

                foreach (var image in data.data)
                {
                    var convertImage = ConvertToBase64(image.imageData, image.fileType);

                    image.imageUrl = convertImage;
                }

                Images = data.data;

                IsLoadding = false;
            }
            catch (Exception error)
            {
                Console.Write(error.Message);
            }
        }

        private string ConvertToBase64(byte[] imageBytes, string fileType)
        {
            return $"data:image/{fileType};base64,{Convert.ToBase64String(imageBytes)}";
        }
    }
}
