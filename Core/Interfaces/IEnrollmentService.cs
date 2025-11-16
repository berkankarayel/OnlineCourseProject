using OnlineCourseApi.Core.DTOs.Request;
using OnlineCourseApi.Core.DTOs.Response;

namespace OnlineCourseApi.Core.Interfaces;

public interface IEnrollmentService
{
    Task<EnrollmentResponseDto> EnrollAsync(int userId, int courseId);
    Task<IEnumerable<EnrollmentResponseDto>> GetUserEnrollmentsAsync(int userId);
    Task<EnrollmentResponseDto> GetEnrollmentByIdAsync(int enrollmentId);
    Task UpdateProgressAsync(int enrollmentId, UpdateProgressDto dto);
    Task<bool> IsUserEnrolledAsync(int userId, int courseId);
}
