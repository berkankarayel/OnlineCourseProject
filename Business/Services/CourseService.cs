using AutoMapper;
using OnlineCourseApi.Core.DTOs.Request;
using OnlineCourseApi.Core.DTOs.Response;
using OnlineCourseApi.Core.Entities;
using OnlineCourseApi.Core.Interfaces;
using OnlineCourseApi.Business.Exceptions;

namespace OnlineCourseApi.Business.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly IGenericRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public CourseService(
        ICourseRepository courseRepository,
        IGenericRepository<Category> categoryRepository,
        IMapper mapper)
    {
        _courseRepository = courseRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<CourseResponseDto> GetByIdAsync(int id)
    {
        var course = await _courseRepository.GetByIdAsync(id);
        
        if (course == null)
            throw new NotFoundException("Kurs bulunamadı!");

        return _mapper.Map<CourseResponseDto>(course);
    }

    public async Task<CourseDetailResponseDto> GetDetailByIdAsync(int id)
    {
        var course = await _courseRepository.GetCourseWithDetailsAsync(id);
        
        if (course == null)
            throw new NotFoundException("Kurs bulunamadı!");

        return _mapper.Map<CourseDetailResponseDto>(course);
    }

    public async Task<IEnumerable<CourseResponseDto>> GetAllAsync()
    {
        var courses = await _courseRepository.GetCoursesWithCategoryAsync();
        return _mapper.Map<IEnumerable<CourseResponseDto>>(courses);
    }

    public async Task<IEnumerable<CourseResponseDto>> GetPublishedCoursesAsync()
    {
        var courses = await _courseRepository.GetPublishedCoursesAsync();
        return _mapper.Map<IEnumerable<CourseResponseDto>>(courses);
    }

    public async Task<IEnumerable<CourseResponseDto>> SearchAsync(string? keyword, string? level, decimal? maxPrice)
    {
        var courses = await _courseRepository.SearchCoursesAsync(keyword, level, maxPrice);
        return _mapper.Map<IEnumerable<CourseResponseDto>>(courses);
    }

    public async Task<CourseResponseDto> CreateAsync(CreateCourseDto dto)
    {
        // Kategori var mı kontrol et
        var categoryExists = await _categoryRepository.ExistsAsync(dto.CategoryId);
        if (!categoryExists)
            throw new NotFoundException("Kategori bulunamadı!");

        var course = _mapper.Map<Course>(dto);
        course.CreatedAt = DateTime.UtcNow;
        course.UpdatedAt = DateTime.UtcNow;

        await _courseRepository.AddAsync(course);
        await _courseRepository.SaveChangesAsync();

        return _mapper.Map<CourseResponseDto>(course);
    }

    public async Task<CourseResponseDto> UpdateAsync(int id, UpdateCourseDto dto)
    {
        var course = await _courseRepository.GetByIdAsync(id);
        
        if (course == null)
            throw new NotFoundException("Kurs bulunamadı!");

        // Kategori değişiyorsa kontrol et
        if (dto.CategoryId != course.CategoryId)
        {
            var categoryExists = await _categoryRepository.ExistsAsync(dto.CategoryId);
            if (!categoryExists)
                throw new NotFoundException("Kategori bulunamadı!");
            
            course.CategoryId = dto.CategoryId;
        }

        // Null olmayan alanları güncelle
        if (!string.IsNullOrWhiteSpace(dto.Title))
            course.Title = dto.Title;
        
        if (!string.IsNullOrWhiteSpace(dto.Description))
            course.Description = dto.Description;
        
        if (!string.IsNullOrWhiteSpace(dto.ShortDescription))
            course.ShortDescription = dto.ShortDescription;
        
        course.Price = dto.Price;
        
        if (!string.IsNullOrWhiteSpace(dto.Level))
            course.Level = dto.Level;
        
        if (!string.IsNullOrWhiteSpace(dto.Status))
            course.Status = dto.Status;

        course.UpdatedAt = DateTime.UtcNow;

        await _courseRepository.UpdateAsync(course);
        await _courseRepository.SaveChangesAsync();

        return _mapper.Map<CourseResponseDto>(course);
    }

    public async Task DeleteAsync(int id)
    {
        var course = await _courseRepository.GetByIdAsync(id);
        
        if (course == null)
            throw new NotFoundException("Kurs bulunamadı!");

        await _courseRepository.DeleteAsync(course);
        await _courseRepository.SaveChangesAsync();
    }
}
