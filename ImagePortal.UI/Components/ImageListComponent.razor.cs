using ImagePortal.Services.UIServices.APIClient;
using ImagePortal.Services.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Net.WebSockets;
using System.Text.Json;

namespace ImagePortal.UI.Components
{
    public partial class ImageListComponent
    {
        [Inject]
        private IImagePortalAPIClient _apiClient { get; set; }
        public List<UIImageDataViewModel> Images { get; set; } = new List<UIImageDataViewModel>();
        public Dictionary<int, string> tags { get; set; } = new Dictionary<int, string>();
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

                Images = data.data;

                //await BuildListFilerTags(Images);

                IsLoadding = false;
            }
            catch (Exception error)
            {
                IsLoadding = false;
                Console.Write(error.Message);
            }
        }

       public async Task BuildListFilerTags(List<UIImageDataViewModel> Images)
        {
            foreach (var image in Images)
            {
                if(!string.IsNullOrEmpty(image.imageMetaDataViewModel.tags))
                {
                    var rawtag = image.imageMetaDataViewModel.tags.Split(",");//JsonSerializer.Deserialize<List<ImageTags>>(image.imageMetaDataViewModel.tags);
                    foreach (var item in rawtag)
                    {

                        if (!tags.ContainsValue(item))
                        {
                            tags.Add(image.imageId, item);
                        }
                    }
                }
            
            
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
