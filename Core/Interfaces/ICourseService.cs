using OnlineCourseApi.Core.DTOs.Request;
using OnlineCourseApi.Core.DTOs.Response;

namespace OnlineCourseApi.Core.Interfaces;

public interface ICourseService
{
    Task<CourseResponseDto> CreateAsync(CreateCourseDto dto);
    Task<CourseResponseDto> UpdateAsync(int id, UpdateCourseDto dto);
    Task<CourseResponseDto> GetByIdAsync(int id);
    Task<CourseDetailResponseDto> GetDetailByIdAsync(int id);
    Task<IEnumerable<CourseResponseDto>> GetAllAsync();
    Task<IEnumerable<CourseResponseDto>> GetPublishedCoursesAsync();
    Task<IEnumerable<CourseResponseDto>> SearchAsync(string? keyword, string? level, decimal? maxPrice);
    Task DeleteAsync(int id);
}
