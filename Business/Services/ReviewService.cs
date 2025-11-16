using AutoMapper;
using OnlineCourseApi.Core.DTOs.Request;
using OnlineCourseApi.Core.DTOs.Response;
using OnlineCourseApi.Core.Entities;
using OnlineCourseApi.Core.Interfaces;
using OnlineCourseApi.Business.Exceptions;

namespace OnlineCourseApi.Business.Services;

public class ReviewService : IReviewService
{
    private readonly IGenericRepository<Review> _reviewRepository;
    private readonly IGenericRepository<Course> _courseRepository;
    private readonly IGenericRepository<User> _userRepository;
    private readonly IGenericRepository<Enrollment> _enrollmentRepository;
    private readonly IMapper _mapper;

    public ReviewService(
        IGenericRepository<Review> reviewRepository,
        IGenericRepository<Course> courseRepository,
        IGenericRepository<User> userRepository,
        IGenericRepository<Enrollment> enrollmentRepository,
        IMapper mapper)
    {
        _reviewRepository = reviewRepository;
        _courseRepository = courseRepository;
        _userRepository = userRepository;
        _enrollmentRepository = enrollmentRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ReviewResponseDto>> GetCourseReviewsAsync(int courseId)
    {
        var reviews = await _reviewRepository.GetAllAsync();
        var courseReviews = reviews.Where(r => r.CourseId == courseId);
        
        return _mapper.Map<IEnumerable<ReviewResponseDto>>(courseReviews);
    }

    public async Task<ReviewResponseDto> AddReviewAsync(int userId, AddReviewDto dto)
    {
        // Kullanıcı var mı?
        var userExists = await _userRepository.ExistsAsync(userId);
        if (!userExists)
            throw new NotFoundException("Kullanıcı bulunamadı!");

        // Kurs var mı?
        var courseExists = await _courseRepository.ExistsAsync(dto.CourseId);
        if (!courseExists)
            throw new NotFoundException("Kurs bulunamadı!");

        // Kullanıcı bu kursa kayıtlı mı? (Sadece kayıtlı kullanıcılar yorum yapabilir)
        var enrollments = await _enrollmentRepository.GetAllAsync();
        var isEnrolled = enrollments.Any(e => e.UserId == userId && e.CourseId == dto.CourseId);
        
        if (!isEnrolled)
            throw new BadRequestException("Bu kursa kayıtlı olmadan yorum yapamazsınız!");

        // Daha önce yorum yapmış mı? (Unique constraint var)
        var reviews = await _reviewRepository.GetAllAsync();
        var existingReview = reviews.FirstOrDefault(r => r.UserId == userId && r.CourseId == dto.CourseId);
        
        if (existingReview != null)
            throw new BadRequestException("Bu kurs için zaten yorum yaptınız!");

        var review = _mapper.Map<Review>(dto);
        review.UserId = userId;
        review.CreatedAt = DateTime.UtcNow;
        review.UpdatedAt = DateTime.UtcNow;

        await _reviewRepository.AddAsync(review);
        await _reviewRepository.SaveChangesAsync();

        return _mapper.Map<ReviewResponseDto>(review);
    }

    public async Task<ReviewResponseDto> UpdateReviewAsync(int userId, int reviewId, AddReviewDto dto)
    {
        var review = await _reviewRepository.GetByIdAsync(reviewId);
        
        if (review == null)
            throw new NotFoundException("Yorum bulunamadı!");

        // Kullanıcı kendi yorumunu mu güncelliyor?
        if (review.UserId != userId)
            throw new UnauthorizedException("Sadece kendi yorumunuzu güncelleyebilirsiniz!");

        review.Rating = dto.Rating;
        review.Comment = dto.Comment;
        review.UpdatedAt = DateTime.UtcNow;

        await _reviewRepository.UpdateAsync(review);
        await _reviewRepository.SaveChangesAsync();

        return _mapper.Map<ReviewResponseDto>(review);
    }

    public async Task<decimal> GetAverageRatingAsync(int courseId)
    {
        var reviews = await _reviewRepository.GetAllAsync();
        var courseReviews = reviews.Where(r => r.CourseId == courseId).ToList();
        
        if (!courseReviews.Any())
            return 0;
        
        return (decimal)courseReviews.Average(r => r.Rating);
    }

    public async Task DeleteReviewAsync(int reviewId)
    {
        var review = await _reviewRepository.GetByIdAsync(reviewId);
        
        if (review == null)
            throw new NotFoundException("Yorum bulunamadı!");

        await _reviewRepository.DeleteAsync(review);
        await _reviewRepository.SaveChangesAsync();
    }
}
