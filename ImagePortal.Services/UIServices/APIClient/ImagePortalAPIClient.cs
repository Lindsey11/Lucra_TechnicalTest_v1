using ImagePortal.Services.ViewModels;
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
    }
}
