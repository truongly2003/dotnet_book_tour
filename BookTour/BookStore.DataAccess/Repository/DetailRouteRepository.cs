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
    public class DetailRouteRepository : IDetailRouteRepository
    {
        private readonly BookTourDbContext _dbContext;
        public DetailRouteRepository(BookTourDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Detailroute> findById(int id)
        {
            return await _dbContext.Detailroutes.FirstOrDefaultAsync(d => d.DetailRouteId == id);
        }
        public async Task<List<Detailroute>> GetAllDetailRouteAsync()
        {
            return await _dbContext.Detailroutes
                .Include(dr => dr.Images) // Eager load Images if needed
                .ToListAsync();
        }
        public async Task<Detailroute> GetDetailRouteByIdAsync(int id)
        {
            var detailRoute = await _dbContext.Detailroutes
                .Include(dr => dr.Images)
                .Include(dr => dr.Legs)
                .Include(dr => dr.Feedbacks)
                .FirstOrDefaultAsync(dr => dr.DetailRouteId == id);
            return detailRoute;
        }

        public async Task<bool> InsertAsync(Detailroute detailroute)
        {
            try
            {
                // Attach and add related entities
                if (detailroute.Images != null && detailroute.Images.Any())
                {
                    foreach (var image in detailroute.Images)
                    {
                        image.DetailRoute = detailroute; // Ensure the relationship is established
                        _dbContext.Entry(image).State = EntityState.Added; // Mark image as added
                    }
                }

                if (detailroute.Legs != null && detailroute.Legs.Any())
                {
                    foreach (var leg in detailroute.Legs)
                    {
                        leg.DetailRoute = detailroute; // Ensure the relationship is established
                        _dbContext.Entry(leg).State = EntityState.Added; // Mark leg as added
                    }
                }

                await _dbContext.Detailroutes.AddAsync(detailroute); // Add the main entity
                var result = await _dbContext.SaveChangesAsync();    // Save changes to the database
                return result > 0; // Return true if one or more rows were affected
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting detail route: {ex.Message}");
                return false;
            }
        }


        public async Task<bool> UpdateAsync(Detailroute detailroute)
        {
            try
            {
                var existingDetailRoute = await _dbContext.Detailroutes
                    .Include(dr => dr.Images)
                    .Include(dr => dr.Legs)
                    .FirstOrDefaultAsync(dr => dr.DetailRouteId == detailroute.DetailRouteId);

                if (existingDetailRoute == null)
                {
                    return false; // Entity doesn't exist
                }

                // Update scalar properties
                existingDetailRoute.DetailRouteName = detailroute.DetailRouteName;
                existingDetailRoute.Price = detailroute.Price;
                existingDetailRoute.Description = detailroute.Description;
                existingDetailRoute.TimeToDeparture = detailroute.TimeToDeparture;
                existingDetailRoute.TimeToFinish = detailroute.TimeToFinish;

                // Update Images if not null
                if (detailroute.Images != null)
                {
                    // Remove images that are not in the new collection
                    var imagesToRemove = existingDetailRoute.Images
                        .Where(ei => !detailroute.Images.Any(ni => ni.ImageId == ei.ImageId))
                        .ToList();

                    _dbContext.Images.RemoveRange(imagesToRemove); // Remove images from DB

                    // Update or add images
                    foreach (var image in detailroute.Images)
                    {
                        var existingImage = existingDetailRoute.Images
                            .FirstOrDefault(ei => ei.ImageId == image.ImageId);

                        if (existingImage == null)
                        {
                            // New image: Add only if it doesn't already exist
                            image.DetailRouteId = existingDetailRoute.DetailRouteId;
                            _dbContext.Images.Add(image); // Add new image
                        }
                        else
                        {
                            // Update existing image properties
                            existingImage.TextImage = image.TextImage;
                            existingImage.IsPrimary = image.IsPrimary;
                        }
                    }
                }

                // Update Legs if not null
                if (detailroute.Legs != null)
                {
                    // Remove legs that are not in the new collection
                    var legsToRemove = existingDetailRoute.Legs
                        .Where(el => !detailroute.Legs.Any(nl => nl.LegId == el.LegId))
                        .ToList();

                    _dbContext.Legs.RemoveRange(legsToRemove); // Remove legs from DB

                    // Update or add legs
                    foreach (var leg in detailroute.Legs)
                    {
                        var existingLeg = existingDetailRoute.Legs
                            .FirstOrDefault(el => el.LegId == leg.LegId);

                        if (existingLeg == null)
                        {
                            // New leg: Add only if it doesn't already exist
                            leg.DetailRouteId = existingDetailRoute.DetailRouteId;
                            _dbContext.Legs.Add(leg); // Add new leg
                        }
                        else
                        {
                            // Update existing leg properties
                            existingLeg.Title = leg.Title;
                            existingLeg.Description = leg.Description;
                            existingLeg.Sequence = leg.Sequence;
                        }
                    }
                }

                // Save changes
                var result = await _dbContext.SaveChangesAsync();
                return result > 0; // Return true if one or more rows were affected
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating detail route: {ex.Message}");
                return false;
            }
        }


        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var detailRoute = await _dbContext.Detailroutes
                    .Include(dr => dr.Images)
                    .Include(dr => dr.Legs)
                    .FirstOrDefaultAsync(dr => dr.DetailRouteId == id);

                if (detailRoute == null)
                {
                    return false; // Entity doesn't exist
                }

                // Remove associated Images
                if (detailRoute.Images != null && detailRoute.Images.Any())
                {
                    _dbContext.Images.RemoveRange(detailRoute.Images);
                }

                // Remove associated Legs
                if (detailRoute.Legs != null && detailRoute.Legs.Any())
                {
                    _dbContext.Legs.RemoveRange(detailRoute.Legs);
                }

                // Remove the main entity
                _dbContext.Detailroutes.Remove(detailRoute);

                var result = await _dbContext.SaveChangesAsync(); // Save changes to the database
                return result > 0; // Return true if one or more rows were affected
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting detail route: {ex.Message}");
                return false;
            }
        }


    }
}
