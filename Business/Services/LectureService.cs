using AutoMapper;
using OnlineCourseApi.Core.DTOs.Request;
using OnlineCourseApi.Core.DTOs.Response;
using OnlineCourseApi.Core.Entities;
using OnlineCourseApi.Core.Interfaces;
using OnlineCourseApi.Business.Exceptions;

namespace OnlineCourseApi.Business.Services;

public class LectureService : ILectureService
{
    private readonly IGenericRepository<Lecture> _lectureRepository;
    private readonly IGenericRepository<Section> _sectionRepository;
    private readonly IMapper _mapper;

    public LectureService(
        IGenericRepository<Lecture> lectureRepository,
        IGenericRepository<Section> sectionRepository,
        IMapper mapper)
    {
        _lectureRepository = lectureRepository;
        _sectionRepository = sectionRepository;
        _mapper = mapper;
    }

    public async Task<LectureResponseDto> GetByIdAsync(int id)
    {
        var lecture = await _lectureRepository.GetByIdAsync(id);
        
        if (lecture == null)
            throw new NotFoundException("Ders bulunamadı!");

        return _mapper.Map<LectureResponseDto>(lecture);
    }

    public async Task<LectureResponseDto> CreateAsync(CreateLectureDto dto)
    {
        // Section var mı kontrol et
        var sectionExists = await _sectionRepository.ExistsAsync(dto.SectionId);
        if (!sectionExists)
            throw new NotFoundException("Bölüm bulunamadı!");

        var lecture = _mapper.Map<Lecture>(dto);
        lecture.CreatedAt = DateTime.UtcNow;

        await _lectureRepository.AddAsync(lecture);
        await _lectureRepository.SaveChangesAsync();

        return _mapper.Map<LectureResponseDto>(lecture);
    }

    public async Task<LectureResponseDto> UpdateAsync(int id, CreateLectureDto dto)
    {
        var lecture = await _lectureRepository.GetByIdAsync(id);
        
        if (lecture == null)
            throw new NotFoundException("Ders bulunamadı!");

        lecture.Title = dto.Title;
        lecture.VideoUrl = dto.VideoUrl;
        lecture.DurationInSeconds = dto.DurationInSeconds;
        lecture.OrderIndex = dto.OrderIndex;
        lecture.IsFree = dto.IsFree;

        await _lectureRepository.UpdateAsync(lecture);
        await _lectureRepository.SaveChangesAsync();

        return _mapper.Map<LectureResponseDto>(lecture);
    }

    public async Task<IEnumerable<LectureResponseDto>> GetBySectionIdAsync(int sectionId)
    {
        var lectures = await _lectureRepository.GetAllAsync();
        var sectionLectures = lectures.Where(l => l.SectionId == sectionId).OrderBy(l => l.OrderIndex);
        
        return _mapper.Map<IEnumerable<LectureResponseDto>>(sectionLectures);
    }

    public async Task DeleteAsync(int id)
    {
        var lecture = await _lectureRepository.GetByIdAsync(id);
        
        if (lecture == null)
            throw new NotFoundException("Ders bulunamadı!");

        await _lectureRepository.DeleteAsync(lecture);
        await _lectureRepository.SaveChangesAsync();
    }
}
