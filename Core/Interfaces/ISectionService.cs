using OnlineCourseApi.Core.DTOs.Response;

namespace OnlineCourseApi.Core.Interfaces;

public interface ISectionService
{
    Task<SectionResponseDto> CreateAsync(int courseId, string title, int orderIndex);
    Task<SectionResponseDto> GetByIdAsync(int id);
    Task<IEnumerable<SectionResponseDto>> GetByCourseIdAsync(int courseId);
    Task DeleteAsync(int id);
}
