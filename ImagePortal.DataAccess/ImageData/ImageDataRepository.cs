using ImagePortal.DataContext.Context;
using ImagePortal.DataContext.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ImagePortal.DataAccess.ImageData
{
    public class ImageDataRepository : IImageDataRepository
    {
        private readonly ImageDataContext _context;

        public ImageDataRepository(ImageDataContext context)
        {
            _context = context;
        }

        public async Task<ImageDatum> CreateAsync(ImageDatum imageDatum)
        {
            _context.ImageData.Add(imageDatum);
            await _context.SaveChangesAsync();
            return imageDatum;
        }

        public async Task CreateMetaData(ImageMetaDatum imageMetaDatum)
        {
            _context.ImageMetaData.Add(imageMetaDatum);
            await _context.SaveChangesAsync();
            
        }
      
        public async Task<List<ImageDatum>> ReadAllImages()
        {
           var allImages = await _context.ImageData.Include(i => i.ImageMetaData).Where(x => x.IsDeleted == false).ToListAsync();
           return allImages;
        }

        public async Task<ImageDatum?> ReadSingleAsync(int imageId)
        {
            return await _context.ImageData.Where(x => x.IsDeleted == false)
                                 .Include(i => i.ImageMetaData)
                                 .FirstOrDefaultAsync(i => i.ImageId == imageId);
        }

        public async Task<bool> UpdateImageAsync(ImageDatum imageDatum)
        {
            try
            {
                var imageToUpdate = await _context.ImageData.FirstOrDefaultAsync(x => x.ImageId == imageDatum.ImageId);

                if (imageToUpdate is not null) 
                {
                    imageToUpdate.Description = imageDatum.Description;
                    //imageToUpdate.Data = imageDatum.Data;
                    imageToUpdate.Title = imageDatum.Title;

                    await _context.SaveChangesAsync();

                    return true;
                }

                return false;
            }
            catch (Exception error)
            {
                throw;
            }
          
        }

        public async Task<bool> DeleteAsync(int imageId)
        {

            try
            {
                var imageToDelete = await _context.ImageData.FirstOrDefaultAsync(x => x.ImageId == imageId);
                if (imageToDelete is not null)
                {
                    imageToDelete.IsDeleted = true;
                    await _context.SaveChangesAsync();

                    return true;
                }

                return false;
            }
            catch (Exception error)
            {

                throw;
            }

            
        }
    }
}
