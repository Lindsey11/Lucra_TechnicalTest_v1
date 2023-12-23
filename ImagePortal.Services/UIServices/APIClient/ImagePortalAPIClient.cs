using Azure.Core;
using ImagePortal.Services.ViewModels;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Extensions.Caching.Memory;
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
using static System.Net.Mime.MediaTypeNames;

namespace ImagePortal.Services.UIServices.APIClient
{
    public class ImagePortalAPIClient : IImagePortalAPIClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;
        public ImagePortalAPIClient(IConfiguration configuration, IMemoryCache cache)
        {
            _configuration = configuration;
            _cache = cache;
        }
        public async  Task<UIServiceResponseModel<List<UIImageDataViewModel>>> GetImages(int pageNumber, int pageSize)
        {
            try
            {
                var myToken = await GetToken();

                var options = new RestClientOptions(_configuration["API:URL"]);
               
                var client = new RestClient(options);
                var request = new RestRequest($"image/get-all-images-paginated?pageNumber={pageNumber}&pageSize={pageSize}", Method.Get);
                request.AddHeader("Authorization", $"Bearer {myToken}");
                
                var response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content;
                    var model = JsonSerializer.Deserialize<UIServiceResponseModel<List<UIImageDataViewModel>>>(data);

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

        public async Task<bool> UpdateImageData(UIImageDataViewModel upload)
        {
            try
            {
                var myToken = await GetToken();
                var options = new RestClientOptions(_configuration["API:URL"]);
                var client = new RestClient(options);
                var request = new RestRequest("image/update-image-data", Method.Put);

                var body = JsonSerializer.Serialize(upload);
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                request.AddHeader("Authorization", $"Bearer {myToken}");

                var response = await client.PutAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
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
                    requestModel.HasMetaData = true;
                    requestModel.ImageMetaDataViewModel = new()
                    {
                        Tags = upload.Tags,
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

        public async Task<UIServiceResponseModel<UIImageDataViewModel>> GetImage(int imageId)
        {
            try
            {
                var myToken = await GetToken();
                var options = new RestClientOptions(_configuration["API:URL"]);
                var client = new RestClient(options);
                var request = new RestRequest($"image/get-single-image?imageId={imageId}", Method.Get);
                request.AddHeader("Authorization", $"Bearer {myToken}");

                var response = await client.GetAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content;
                    var model = JsonSerializer.Deserialize<UIServiceResponseModel<UIImageDataViewModel>>(data);

                    return model;
                }
                else
                {
                    return new() { messagae = "Error fetching data", success = false };
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<bool> DeleteImnage(int imageId)
        {
            try
            {
                var myToken = await GetToken();
                var options = new RestClientOptions(_configuration["API:URL"]);
                var client = new RestClient(options);
                var request = new RestRequest($"image/delete-image?imageId={imageId}", Method.Delete);
                request.AddHeader("Authorization", $"Bearer {myToken}");
                var response = await client.DeleteAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                   return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<string> GetToken()
        {
            //lets first check if we have a token in cache
            //var cacheToken = await CheckCacheForToken();

            //if (!string.IsNullOrEmpty(cacheToken))
            //{
            //    return cacheToken;
            //}

            //or else lets just get a new one
            var options = new RestClientOptions(_configuration["API:URL"]);
            
            var client = new RestClient(options);
            var request = new RestRequest($"token/get-token", Method.Get);

            var response = await client.GetAsync<string>(request);
            var token = response;
            return token;
        }

        private async Task<string> CheckCacheForToken()
        {
            var key = "Token";
            var token = "";
            if(_cache.TryGetValue(key, out token))
            {
                return token;
            }
            else
            {
                return null;
            }
        }

        private async Task SetTokenCache(string token)
        {
            var options = new MemoryCacheEntryOptions()
                                .SetSlidingExpiration(TimeSpan.FromMinutes(10))
                                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                                .SetSize(156);

            _cache.Set("Token", token, options);
        }

        
    }
}
