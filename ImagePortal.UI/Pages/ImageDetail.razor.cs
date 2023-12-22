using ImagePortal.Services.UIServices.APIClient;
using ImagePortal.Services.ViewModels;
using ImagePortal.UI.Components;
using Microsoft.AspNetCore.Components;

namespace ImagePortal.UI.Pages
{
    public partial class ImageDetail
    {
        [Inject]
        private IImagePortalAPIClient _apiClient { get; set; }
        [Inject]
        private NavigationManager _navigationManager { get; set; }
        private CustomDialog dialog { get; set; }

        [Parameter]
        public int deviceId { get; set; }
        public UIImageDataViewModel ImageDataViewModel { get; set; }
        public bool IsLoading { get; set; }
        protected override async Task OnParametersSetAsync()
        {
            await GetImageDetail();
        }

        public async Task GetImageDetail()
        {
            IsLoading = true;
            var Image = await _apiClient.GetImage(deviceId);
            if (Image.success)
            {
                ImageDataViewModel = Image.data;
              
            }
            else
            {
                //Show error message
                Console.WriteLine(Image.messagae);
            }

            IsLoading = false;
        }

        private async Task HandleValidSubmit()
        {
            var updateResult = await _apiClient.UpdateImageData(ImageDataViewModel);

            if (updateResult)
            {
                //Navigate back home
                _navigationManager.NavigateTo("/");
            }
            else
            {
                //show error
                dialog.Show("Delete", "Are you sure you want to delete this image?");
            }
        }

        public async Task Delete()
        {
            dialog.Show("Delete", "Are you sure you want to delete this image?");
        }

        private void HandleConfirmation(bool confirmed)
        {
            if (confirmed)
            {
                
            }
        }
    }
}
