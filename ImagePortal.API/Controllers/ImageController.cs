using ImagePortal.Services.ApiServices;
using ImagePortal.Services.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImagePortal.API.Controllers
{
    [Route("image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageDataService _imageService;
        public ImageController(IImageDataService imageDataService) 
        {
            _imageService = imageDataService;
        }

        [HttpGet("get-all-images")]
        [ProducesResponseType(typeof(APIServiceResponseModel<List<ImageDataViewModel>>),200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> GetAllImages()
        {
            try
            {
                var res = await _imageService.GetAllImageData();
                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet("get-all-images-paginated")]
        [ProducesResponseType(typeof(APIServiceResponseModel<List<ImageDataViewModel>>), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> GetAllImagesPaginated(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var res = await _imageService.GetAllImageDataPaginated(pageNumber, pageSize);
                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet("get-single-image")]
        [ProducesResponseType(typeof(APIServiceResponseModel<ImageDataViewModel>), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> GetSingleImage(int imageId)
        {
            try
            {
                var res = await _imageService.GetImageData(imageId);
                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPost("add-new-image")]
        [ProducesResponseType(typeof(APIServiceResponseModel<bool>), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreateNewImage([FromBody] ImageDataViewModel request)
        {
            try
            {

                //request.ImageData = await ExtractByteArray(request.file);
                var res = await _imageService.CreateNewImage(request);
                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPut("update-image-data")]
        [ProducesResponseType(typeof(APIServiceResponseModel<bool>), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> UpdateImage([FromForm] ImageDataViewModel request)
        {
            try
            {

                var res = await _imageService.UpdateImage(request);
                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPut("update-image-metadata")]
        [ProducesResponseType(typeof(APIServiceResponseModel<bool>), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> UpdateMetaData([FromBody] ImageMetaDataViewModel request)
        {
            try
            {
                var res = await _imageService.UpdateMetaData(request);
                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpDelete("delete-image")]
        [ProducesResponseType(typeof(APIServiceResponseModel<bool>), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> DeleteImage(int imageid)
        {
            try
            {
                var res = await _imageService.DeleteImage(imageid);
                return Ok(res);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }


        private async Task<byte[]> ExtractByteArray(IFormFile file)
        {
            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }

            return fileBytes;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }

            
            return Ok(new { file.FileName, FileContent = fileBytes });
        }
    }
}
