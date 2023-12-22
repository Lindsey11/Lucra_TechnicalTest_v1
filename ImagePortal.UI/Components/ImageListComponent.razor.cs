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
        public List<string> tags { get; set; } = new List<string>();
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

                await BuildListFilerTags(Images);

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
                if(image.imageMetaDataViewModel.tags is not null)
                {
                    var rawtag = JsonSerializer.Deserialize<List<ImageTags>>(image.imageMetaDataViewModel.tags);
                    foreach (var item in rawtag)
                    {

                        if (!tags.Contains(item.TagName))
                        {
                            tags.Add(item.TagName);
                        }
                    }
                }
            
            
            }
        }
    }
}
