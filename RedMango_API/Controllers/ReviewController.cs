using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedMango_API.Data;
using RedMango_API.Models;
using RedMango_API.Models.Dto;
using System.Net;

namespace RedMango_API.Controllers
{
    [Route("api/review")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private ApiResponse _response;
        public ReviewController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ApiResponse();
        }

        [HttpGet("getReviews/{menuItemId}")]
        public async Task<IActionResult> GetAllReviews(int menuItemId)
        {
            try
            {
                IEnumerable<Review> lstReviews = await _db.Reviews.Where(u => u.MenuItemId == menuItemId).ToListAsync();
                if(lstReviews.Count() <= 0)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("This menu item doesn't have any review");
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = lstReviews;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.BadRequest;
            }
            return BadRequest(_response);
        }

        [HttpGet("getAverage/{menuItemId:int}")]
        public async Task<ActionResult<ApiResponse>> AverageRating(int menuItemId)
        {
            try
            {
                IEnumerable<Review> lstReviews = await _db.Reviews.Where(u => u.MenuItemId == menuItemId).ToListAsync();
                if (lstReviews.Count() <= 0)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("This menu item doesn't have any review");
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                AverageRatingDTO avgRating = new()
                {
                    noOneStar = lstReviews.Where(u => u.Stars == 1).Count(),
                    noTwoStars = lstReviews.Where(u => u.Stars == 2).Count(),
                    noThreeStars = lstReviews.Where(u => u.Stars == 3).Count(),
                    noFourStars = lstReviews.Where(u => u.Stars == 4).Count(),
                    noFiveStars = lstReviews.Where(u => u.Stars == 5).Count(),
                };
                avgRating.totalRating = avgRating.noOneStar + avgRating.noTwoStars + avgRating.noThreeStars +  avgRating.noFourStars + avgRating.noFiveStars;
                avgRating.averageRating = (avgRating.noOneStar * 1 + avgRating.noTwoStars * 2 + avgRating.noThreeStars * 3 + avgRating.noFourStars * 4 + avgRating.noFiveStars * 5) / avgRating.totalRating;
                _response.Result = avgRating;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(ex.Message);
                _response.StatusCode = HttpStatusCode.BadRequest;
            }
            return BadRequest(_response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateReview([FromForm] CreateReviewDTO createReviewDTO)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    Review review = _mapper.Map<Review>(createReviewDTO);
                    review.CreatedAt = DateTime.Now;
                    _db.Reviews.Add(review);
                    await _db.SaveChangesAsync();

                    _response.Result = review;
                    _response.StatusCode = HttpStatusCode.OK;
                    return Ok(_response);
                }
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add(ex.Message);
            }
            return BadRequest(_response);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<ApiResponse>> DeleteReview(int id)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    Review review = await _db.Reviews.FirstOrDefaultAsync(u => u.Id == id);
                    _db.Reviews.Remove(review);
                    await _db.SaveChangesAsync();
                    _response.Result = true;
                    _response.StatusCode = HttpStatusCode.OK;
                    return Ok(_response);
                }
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add(ex.Message);
            }
            return BadRequest(_response);
        }
    }
}
