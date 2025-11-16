using OnlineCourseApi.Core.DTOs.Request;
using OnlineCourseApi.Core.DTOs.Response;

namespace OnlineCourseApi.Core.Interfaces;

public interface ICategoryService
{
    Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto);
    Task<CategoryResponseDto> GetByIdAsync(int id);
    Task<IEnumerable<CategoryResponseDto>> GetAllAsync();
    Task DeleteAsync(int id);
}
