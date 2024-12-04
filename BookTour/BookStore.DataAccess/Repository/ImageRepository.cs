using BookTour.Domain.Entity;
using BookTour.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly BookTourDbContext _dbContext;
        public ImageRepository(BookTourDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Image>> GetImageByDetailRouteIdAsync(int detailRouteId)
        {
            var query =await _dbContext.Images
                .Where(img=>img.DetailRouteId == detailRouteId)
                .ToListAsync();
            return query;
        }

        public async Task<bool> InsertAsync(Image image)
        {
            try
            {
                await _dbContext.Images.AddAsync(image);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting image: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Image image)
        {
            try
            {
                var existingImage = await _dbContext.Images.FindAsync(image.ImageId);
                if (existingImage == null) return false;

                existingImage.TextImage = image.TextImage;
                existingImage.IsPrimary = image.IsPrimary;

                _dbContext.Images.Update(existingImage);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating image: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int imageId)
        {
            try
            {
                var image = await _dbContext.Images.FindAsync(imageId);
                if (image == null) return false;

                _dbContext.Images.Remove(image);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting image: {ex.Message}");
                return false;
            }
        }
    }

}
