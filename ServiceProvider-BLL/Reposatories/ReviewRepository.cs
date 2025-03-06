using Azure.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;
using SeeviceProvider_BLL.Abstractions;
using ServiceProvider_BLL.Dtos.ReviewDto;
using ServiceProvider_BLL.Errors;
using ServiceProvider_BLL.Interfaces;
using ServiceProvider_DAL.Data;
using ServiceProvider_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider_BLL.Reposatories
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        private readonly AppDbContext _context;

        public ReviewRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Result> UpdateReviewAsync(int reviewId, UpdateReviewRequest request, CancellationToken cancellationToken = default)
        {
            var review = await _context.Reviews!.FindAsync(reviewId, cancellationToken);

            if(review == null)
                return Result.Failure(ReviewErrors.ReviewNotFound);

            if (review.ApplicationUserId != request.UserId)
                return Result.Failure(ReviewErrors.Forbid);

            review.Rating = request.Rating;
            review.Comment = request.Comment;
            
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }

        public async Task<Result> DeleteReviewAsync(int reviewId, string userId, CancellationToken cancellationToken = default)
        {
            var review = await _context.Reviews!.FindAsync(reviewId,cancellationToken);
           
            if (review == null)
                return Result.Failure(ReviewErrors.ReviewNotFound);

            if (review.ApplicationUserId != userId)
                return Result.Failure(ReviewErrors.Forbid);

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }
        
 
    }
}
