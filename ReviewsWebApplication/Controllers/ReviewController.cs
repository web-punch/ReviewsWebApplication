using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reviews.Domain.Interfaces;
using Reviews.Domain.Models;
using ReviewsWebApplication.Models;

namespace ReviewsWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ReviewController : ControllerBase
    {

        private readonly ILogger<ReviewController> _logger;
        private readonly IReviewService reviewService;

        public ReviewController(ILogger<ReviewController> logger, IReviewService reviewService)
        {
            _logger = logger;
            this.reviewService = reviewService;
        }

        /// <summary>
        /// ��������� ���� ������� �� ��������
        /// </summary>
        /// <param name="productId">Id ��������</param>
        /// <returns>��� �������� ���������� ��������� ������� ����� BadReqest � ��������� ������</returns>
        [HttpGet("GetReviewsByProductId")]
        public async Task<ActionResult<List<Review>>> TryGetAllReviewsAsync(Guid productId)
        {
            try
            {
                var reviews = await reviewService.GetReviewsByProductIdAsync(productId);
                return Ok(reviews);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return BadRequest(new { Error = e.Message });
            }
        }

        /// <summary>
        /// ��������� ������
        /// </summary>
        /// <param name="reviewId">Id ������</param>
        /// <returns>��� �������� ���������� ����� ����� BadReqest � ��������� ������</returns>
        [HttpGet("GetReview")]
        public async Task<ActionResult<Review>> TryGetReviewAsync(Guid reviewId)
        {
            try
            {
                var review = await reviewService.GetReviewAsync(reviewId);
                return Ok(review);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return BadRequest(new { Error = e.Message });
            }
        }

        /// <summary>
        /// �������� ������
        /// </summary>
        /// <param name="reviewId">Id ������</param>
        /// <returns>��� �������� ���������� ActionResult Ok ����� BadReqest � ��������� ������</returns>
        [Authorize]
        [HttpDelete("DeleteReview")]
        public async Task<ActionResult<bool>> TryDeleteReviewAsync(Guid reviewId)
        {
            try
            {
                var result = await reviewService.TryDeleteReviewAsync(reviewId);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return BadRequest(new { Error = e.Message });
            }
        }

        /// <summary>
        /// ���������� ������
        /// </summary>
        /// <param name="review">������ ������ ��� �����������</param>
        /// <returns>��� �������� ���������� ActionResult Ok ����� BadReqest � ��������� ������</returns>
        [Authorize]
        [HttpPost("AddReview")]
        public async Task<ActionResult<bool>> TryAddReviewAsync(AddReviewModel review)
        {
            try
            {
                var newReview = new Review
                {
                    ProductId = review.ProductId,
                    UserId = review.UserId,
                    Text = review.Text,
                    Grade = review.Grade,
                    CreateDate = DateTime.Now,
                    Status = Status.Actual,
                };
                var result = await reviewService.TryAddReviewAsync(newReview);
                return Ok(result);
            }
            catch(Exception ex) 
            {
                _logger?.LogError(ex.Message, ex);
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}