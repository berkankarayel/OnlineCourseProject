using OnlineCourseApi.Core.DTOs.Request;
using OnlineCourseApi.Core.DTOs.Response;

namespace OnlineCourseApi.Core.Interfaces;

public interface ILectureService
{
    Task<LectureResponseDto> CreateAsync(CreateLectureDto dto);
    Task<LectureResponseDto> GetByIdAsync(int id);
    Task<IEnumerable<LectureResponseDto>> GetBySectionIdAsync(int sectionId);
    Task DeleteAsync(int id);
}
