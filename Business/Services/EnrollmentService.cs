using AutoMapper;
using OnlineCourseApi.Core.DTOs.Request;
using OnlineCourseApi.Core.DTOs.Response;
using OnlineCourseApi.Core.Entities;
using OnlineCourseApi.Core.Interfaces;
using OnlineCourseApi.Business.Exceptions;

namespace OnlineCourseApi.Business.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IGenericRepository<Enrollment> _enrollmentRepository;
    private readonly IGenericRepository<Course> _courseRepository;
    private readonly IGenericRepository<User> _userRepository;
    private readonly IGenericRepository<LectureProgress> _progressRepository;
    private readonly IMapper _mapper;

    public EnrollmentService(
        IGenericRepository<Enrollment> enrollmentRepository,
        IGenericRepository<Course> courseRepository,
        IGenericRepository<User> userRepository,
        IGenericRepository<LectureProgress> progressRepository,
        IMapper mapper)
    {
        _enrollmentRepository = enrollmentRepository;
        _courseRepository = courseRepository;
        _userRepository = userRepository;
        _progressRepository = progressRepository;
        _mapper = mapper;
    }

    public async Task<EnrollmentResponseDto> EnrollAsync(int userId, int courseId)
    {
        // Kullanıcı var mı?
        var userExists = await _userRepository.ExistsAsync(userId);
        if (!userExists)
            throw new NotFoundException("Kullanıcı bulunamadı!");

        // Kurs var mı?
        var courseExists = await _courseRepository.ExistsAsync(courseId);
        if (!courseExists)
            throw new NotFoundException("Kurs bulunamadı!");

        // Daha önce kayıt olmuş mu? (Unique constraint var ama kontrol edelim)
        var enrollments = await _enrollmentRepository.GetAllAsync();
        var existingEnrollment = enrollments.FirstOrDefault(e => e.UserId == userId && e.CourseId == courseId);
        
        if (existingEnrollment != null)
            throw new BadRequestException("Bu kursa zaten kayıtlısınız!");

        var enrollment = new Enrollment
        {
            UserId = userId,
            CourseId = courseId,
            EnrolledAt = DateTime.UtcNow,
            ProgressPercentage = 0,
            IsCompleted = false
        };

        await _enrollmentRepository.AddAsync(enrollment);
        await _enrollmentRepository.SaveChangesAsync();

        return _mapper.Map<EnrollmentResponseDto>(enrollment);
    }

    public async Task<EnrollmentResponseDto> GetEnrollmentByIdAsync(int enrollmentId)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(enrollmentId);
        
        if (enrollment == null)
            throw new NotFoundException("Kayıt bulunamadı!");
        
        return _mapper.Map<EnrollmentResponseDto>(enrollment);
    }

    public async Task<IEnumerable<EnrollmentResponseDto>> GetUserEnrollmentsAsync(int userId)
    {
        var enrollments = await _enrollmentRepository.GetAllAsync();
        var userEnrollments = enrollments.Where(e => e.UserId == userId);
        
        return _mapper.Map<IEnumerable<EnrollmentResponseDto>>(userEnrollments);
    }

    public async Task<bool> IsUserEnrolledAsync(int userId, int courseId)
    {
        var enrollments = await _enrollmentRepository.GetAllAsync();
        return enrollments.Any(e => e.UserId == userId && e.CourseId == courseId);
    }

    public async Task UpdateProgressAsync(int enrollmentId, UpdateProgressDto dto)
    {
        var enrollment = await _enrollmentRepository.GetByIdAsync(enrollmentId);
        
        if (enrollment == null)
            throw new NotFoundException("Kayıt bulunamadı!");

        // LectureProgress kaydı oluştur veya güncelle
        var progresses = await _progressRepository.GetAllAsync();
        var progress = progresses.FirstOrDefault(p => 
            p.EnrollmentId == enrollment.Id && p.LectureId == dto.LectureId);

        if (progress == null)
        {
            progress = new LectureProgress
            {
                EnrollmentId = enrollment.Id,
                LectureId = dto.LectureId,
                IsCompleted = dto.IsCompleted,
                WatchedPercentage = dto.WatchedPercentage,
                LastWatchedAt = DateTime.UtcNow
            };
            await _progressRepository.AddAsync(progress);
        }
        else
        {
            progress.IsCompleted = dto.IsCompleted;
            progress.WatchedPercentage = dto.WatchedPercentage;
            progress.LastWatchedAt = DateTime.UtcNow;
            await _progressRepository.UpdateAsync(progress);
        }

        // Enrollment progress percentage hesapla (tüm lecture'ları kontrol ederek)
        var allProgresses = progresses.Where(p => p.EnrollmentId == enrollment.Id).ToList();
        var completedCount = allProgresses.Count(p => p.IsCompleted);
        var totalCount = allProgresses.Count;
        
        if (totalCount > 0)
        {
            enrollment.ProgressPercentage = (decimal)completedCount / totalCount * 100;
            enrollment.IsCompleted = enrollment.ProgressPercentage >= 100;
            
            if (enrollment.IsCompleted && enrollment.CompletedAt == null)
                enrollment.CompletedAt = DateTime.UtcNow;
        }

        await _enrollmentRepository.UpdateAsync(enrollment);
        await _progressRepository.SaveChangesAsync();
        await _enrollmentRepository.SaveChangesAsync();
    }
}
