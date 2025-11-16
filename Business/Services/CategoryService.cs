using AutoMapper;
using OnlineCourseApi.Core.DTOs.Request;
using OnlineCourseApi.Core.DTOs.Response;
using OnlineCourseApi.Core.Entities;
using OnlineCourseApi.Core.Interfaces;
using OnlineCourseApi.Business.Exceptions;

namespace OnlineCourseApi.Business.Services;

public class CategoryService : ICategoryService
{
    private readonly IGenericRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(IGenericRepository<Category> categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<CategoryResponseDto> GetByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        
        if (category == null)
            throw new NotFoundException("Kategori bulunamadı!");

        return _mapper.Map<CategoryResponseDto>(category);
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CategoryResponseDto>>(categories);
    }

    public async Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto)
    {
        var category = _mapper.Map<Category>(dto);
        category.CreatedAt = DateTime.UtcNow;

        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveChangesAsync();

        return _mapper.Map<CategoryResponseDto>(category);
    }

    public async Task<CategoryResponseDto> UpdateAsync(int id, CreateCategoryDto dto)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        
        if (category == null)
            throw new NotFoundException("Kategori bulunamadı!");

        category.Name = dto.Name;
        category.Description = dto.Description;

        await _categoryRepository.UpdateAsync(category);
        await _categoryRepository.SaveChangesAsync();

        return _mapper.Map<CategoryResponseDto>(category);
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        
        if (category == null)
            throw new NotFoundException("Kategori bulunamadı!");

        await _categoryRepository.DeleteAsync(category);
        await _categoryRepository.SaveChangesAsync();
    }
}
