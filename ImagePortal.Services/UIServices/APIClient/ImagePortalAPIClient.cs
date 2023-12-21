using ImagePortal.Services.ViewModels;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Extensions.Configuration;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ImagePortal.Services.UIServices.APIClient
{
    public class ImagePortalAPIClient : IImagePortalAPIClient
    {
        private readonly IConfiguration _configuration;
        public ImagePortalAPIClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async  Task<UIServiceResponseModel> GetImages(int pageNumber, int pageSize)
        {
            try
            {
                var options = new RestClientOptions(_configuration["API:URL"]);
                {
                    //Authenticator = new HttpBasicAuthenticator("username", "password")
                };
                var client = new RestClient(options);
                var request = new RestRequest("image/get-all-images-paginated", Method.Get);
                // The cancellation token comes from the caller. You can still make a call without it.
                var response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content;
                    var model = JsonSerializer.Deserialize<UIServiceResponseModel>(data);

                    return model;
                }
                else
                {
                    return new() { messagae = "Error fetching data", success = false };
                }
            }
            catch (Exception error)
            {
                throw;
            }
         
        }

        public async Task<bool> UploadImage(ImageUploadModel upload)
        {
            try
            {
                var options = new RestClientOptions(_configuration["API:URL"]);
                {
                    //Authenticator = new HttpBasicAuthenticator("username", "password")
                };
                var client = new RestClient(options);
                var request = new RestRequest("image/add-new-image", Method.Post);

                var requestModel = new ImageDataViewModel()
                {
                    Title = upload.Title,
                    Description = upload.Description,
                    FileType = upload.FileType,
                    ImageData = upload.ImageData,
                    ImageId = 0

                };

                if (!string.IsNullOrEmpty(upload.Tags))
                {
                    var tags = new List<ImageTags>();
                    foreach (var tag in upload.Tags.Split(","))
                    {
                        tags.Add(new ImageTags()
                        {
                            TagName = tag,
                        });
                    }

                    var tagsJson = JsonSerializer.Serialize(tags);
                    requestModel.HasMetaData = true;
                    requestModel.ImageMetaDataViewModel = new()
                    {
                        Tags = tagsJson,
                        Categories = upload.Categories,
                        ImageMetaDataId = 0
                    };
                }

                var body = JsonSerializer.Serialize(requestModel);
                request.AddParameter("application/json", body, ParameterType.RequestBody);

                var response = await client.PostAsync(request);

                return true;
            }
            catch (Exception error)
            {
                throw;
            }
        }
    }
}
