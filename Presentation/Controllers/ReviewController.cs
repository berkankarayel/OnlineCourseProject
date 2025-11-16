using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using OnlineCourseApi.Core.DTOs.Request;
using OnlineCourseApi.Core.Interfaces;

namespace OnlineCourseApi.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet("course/{courseId}")]
    public async Task<IActionResult> GetCourseReviews(int courseId)
    {
        var result = await _reviewService.GetCourseReviewsAsync(courseId);
        return Ok(result);
    }

    [HttpGet("course/{courseId}/average-rating")]
    public async Task<IActionResult> GetAverageRating(int courseId)
    {
        var result = await _reviewService.GetAverageRatingAsync(courseId);
        return Ok(new { averageRating = result });
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddReview([FromBody] AddReviewDto dto)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _reviewService.AddReviewAsync(userId, dto);
        return CreatedAtAction(nameof(GetCourseReviews), new { courseId = dto.CourseId }, result);
    }

    [HttpDelete("{reviewId}")]
    [Authorize]
    public async Task<IActionResult> DeleteReview(int reviewId)
    {
        await _reviewService.DeleteReviewAsync(reviewId);
        return NoContent();
    }
}
