using Microsoft.EntityFrameworkCore;
using OnlineCourseApi.Core.Entities;
using OnlineCourseApi.Core.Interfaces;
using OnlineCourseApi.Data.Context;

namespace OnlineCourseApi.Data.Repositories;

public class CourseRepository : GenericRepository<Course>, ICourseRepository
{
    private readonly AppDbContext _context;

    public CourseRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Course>> GetCoursesWithCategoryAsync()
    {
        return await _context.Courses
            .Include(c => c.Category)
            .ToListAsync();
    }

    public async Task<IEnumerable<Course>> GetPublishedCoursesAsync()
    {
        return await _context.Courses
            .Include(c => c.Category)
            .Where(c => c.Status == "Published")
            .ToListAsync();
    }

    public async Task<Course?> GetCourseWithDetailsAsync(int courseId)
    {
        return await _context.Courses
            .Include(c => c.Category)
            .Include(c => c.Sections)
                .ThenInclude(s => s.Lectures)
            .Include(c => c.Reviews)
            .FirstOrDefaultAsync(c => c.Id == courseId);
    }

    public async Task<IEnumerable<Course>> SearchCoursesAsync(string? keyword, string? level, decimal? maxPrice)
    {
        var query = _context.Courses
            .Include(c => c.Category)
            .Where(c => c.Status == "Published")
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(c => c.Title.Contains(keyword) || c.Description.Contains(keyword));
        }

        if (!string.IsNullOrWhiteSpace(level))
        {
            query = query.Where(c => c.Level == level);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(c => c.Price <= maxPrice.Value);
        }

        return await query.ToListAsync();
    }
}
