using OnlineCourseApi.Core.Entities;

namespace OnlineCourseApi.Core.Interfaces;

public interface ICourseRepository : IGenericRepository<Course>
{
    Task<IEnumerable<Course>> GetCoursesWithCategoryAsync();
    Task<IEnumerable<Course>> GetPublishedCoursesAsync();
    Task<Course?> GetCourseWithDetailsAsync(int courseId);
    Task<IEnumerable<Course>> SearchCoursesAsync(string? keyword, string? level, decimal? maxPrice);
}
