using OnlineCourseApi.Core.DTOs.Request;
using OnlineCourseApi.Core.DTOs.Response;

namespace OnlineCourseApi.Core.Interfaces;

public interface IReviewService
{
    Task<ReviewResponseDto> AddReviewAsync(int userId, AddReviewDto dto);
    Task<IEnumerable<ReviewResponseDto>> GetCourseReviewsAsync(int courseId);
    Task<decimal> GetAverageRatingAsync(int courseId);
    Task DeleteReviewAsync(int reviewId);
}
