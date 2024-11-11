using Microsoft.EntityFrameworkCore;
using Reviews.Domain.Interfaces;
using Reviews.Domain.Models;

namespace Reviews.Domain.Services
{
    public class ReviewService : IReviewService
    {
        private readonly DataBaseContext databaseContext;

        public ReviewService(DataBaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public async Task<List<Review>> GetReviewsByProductIdAsync(Guid productId)
        {
            return await databaseContext.Reviews.Where(review => review.ProductId == productId && review.Status == Status.Actual).ToListAsync();
        }

        public async Task<Review?> GetReviewAsync(Guid reviewId)
        {
            return await databaseContext.Reviews.FirstOrDefaultAsync(review => review.Id == reviewId && review.Status == Status.Actual);
        }

        public async Task<bool> TryDeleteReviewAsync(Guid reviewId)
        {
            try
            {
                var review = await databaseContext.Reviews.FirstOrDefaultAsync(review => review.Id == reviewId);
                review.Status = Status.Deleted;
                await databaseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> TryAddReviewAsync(Review review)
        {
            try
            {
                await databaseContext.Reviews.AddAsync(review);
                await databaseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
