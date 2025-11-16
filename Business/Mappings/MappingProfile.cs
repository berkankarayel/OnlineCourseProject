using AutoMapper;
using OnlineCourseApi.Core.Entities;
using OnlineCourseApi.Core.DTOs.Request;
using OnlineCourseApi.Core.DTOs.Response;

namespace OnlineCourseApi.Business.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User Mappings
        CreateMap<User, UserResponseDto>();
        CreateMap<RegisterRequestDto, User>();
        CreateMap<UpdateUserDto, User>();

        // Category Mappings
        CreateMap<Category, CategoryResponseDto>();
        CreateMap<CreateCategoryDto, Category>();

        // Course Mappings
        CreateMap<Course, CourseResponseDto>()
            .ForMember(dest => dest.CategoryName, 
                       opt => opt.MapFrom(src => src.Category.Name));

        CreateMap<Course, CourseDetailResponseDto>()
            .ForMember(dest => dest.CategoryName, 
                       opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.Sections, 
                       opt => opt.MapFrom(src => src.Sections))
            .ForMember(dest => dest.TotalLectures, 
                       opt => opt.MapFrom(src => src.Sections.SelectMany(s => s.Lectures).Count()))
            .ForMember(dest => dest.TotalDurationInSeconds, 
                       opt => opt.MapFrom(src => src.Sections.SelectMany(s => s.Lectures).Sum(l => l.DurationInSeconds)))
            .ForMember(dest => dest.AverageRating, 
                       opt => opt.MapFrom(src => src.Reviews.Any() ? src.Reviews.Average(r => r.Rating) : 0))
            .ForMember(dest => dest.TotalReviews, 
                       opt => opt.MapFrom(src => src.Reviews.Count));

        CreateMap<CreateCourseDto, Course>();
        CreateMap<UpdateCourseDto, Course>();

        // Section Mappings
        CreateMap<Section, SectionResponseDto>()
            .ForMember(dest => dest.Lectures, 
                       opt => opt.MapFrom(src => src.Lectures));
        CreateMap<CreateSectionDto, Section>();

        // Lecture Mappings
        CreateMap<Lecture, LectureResponseDto>();
        CreateMap<CreateLectureDto, Lecture>();

        // Enrollment Mappings
        CreateMap<Enrollment, EnrollmentResponseDto>()
            .ForMember(dest => dest.CourseTitle, 
                       opt => opt.MapFrom(src => src.Course.Title))
            .ForMember(dest => dest.CourseThumbnailUrl, 
                       opt => opt.MapFrom(src => src.Course.ThumbnailUrl));

        // Review Mappings
        CreateMap<Review, ReviewResponseDto>()
            .ForMember(dest => dest.UserFirstName, 
                       opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.UserLastName, 
                       opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.UserProfileImageUrl, 
                       opt => opt.MapFrom(src => src.User.ProfileImageUrl));
        CreateMap<AddReviewDto, Review>();

        // Certificate Mappings
        CreateMap<Certificate, CertificateResponseDto>()
            .ForMember(dest => dest.CourseTitle, 
                       opt => opt.MapFrom(src => src.Course.Title));
    }
}
