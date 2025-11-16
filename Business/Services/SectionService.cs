using AutoMapper;
using OnlineCourseApi.Core.DTOs.Request;
using OnlineCourseApi.Core.DTOs.Response;
using OnlineCourseApi.Core.Entities;
using OnlineCourseApi.Core.Interfaces;
using OnlineCourseApi.Business.Exceptions;

namespace OnlineCourseApi.Business.Services;

public class SectionService : ISectionService
{
    private readonly IGenericRepository<Section> _sectionRepository;
    private readonly IGenericRepository<Course> _courseRepository;
    private readonly IMapper _mapper;

    public SectionService(
        IGenericRepository<Section> sectionRepository,
        IGenericRepository<Course> courseRepository,
        IMapper mapper)
    {
        _sectionRepository = sectionRepository;
        _courseRepository = courseRepository;
        _mapper = mapper;
    }

    public async Task<SectionResponseDto> GetByIdAsync(int id)
    {
        var section = await _sectionRepository.GetByIdAsync(id);
        
        if (section == null)
            throw new NotFoundException("Bölüm bulunamadı!");

        return _mapper.Map<SectionResponseDto>(section);
    }

    public async Task<SectionResponseDto> CreateAsync(int courseId, string title, int orderIndex)
    {
        // Kurs var mı kontrol et
        var courseExists = await _courseRepository.ExistsAsync(courseId);
        if (!courseExists)
            throw new NotFoundException("Kurs bulunamadı!");

        var section = new Section
        {
            CourseId = courseId,
            Title = title,
            OrderIndex = orderIndex,
            CreatedAt = DateTime.UtcNow
        };

        await _sectionRepository.AddAsync(section);
        await _sectionRepository.SaveChangesAsync();

        return _mapper.Map<SectionResponseDto>(section);
    }

    public async Task<IEnumerable<SectionResponseDto>> GetByCourseIdAsync(int courseId)
    {
        var sections = await _sectionRepository.GetAllAsync();
        var courseSections = sections.Where(s => s.CourseId == courseId).OrderBy(s => s.OrderIndex);
        
        return _mapper.Map<IEnumerable<SectionResponseDto>>(courseSections);
    }

    public async Task DeleteAsync(int id)
    {
        var section = await _sectionRepository.GetByIdAsync(id);
        
        if (section == null)
            throw new NotFoundException("Bölüm bulunamadı!");

        await _sectionRepository.DeleteAsync(section);
        await _sectionRepository.SaveChangesAsync();
    }
}
