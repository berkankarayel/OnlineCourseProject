using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using OnlineCourseApi.Core.DTOs.Request;
using OnlineCourseApi.Core.Interfaces;

namespace OnlineCourseApi.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EnrollmentController : ControllerBase
{
    private readonly IEnrollmentService _enrollmentService;

    public EnrollmentController(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    [HttpPost("enroll/{courseId}")]
    public async Task<IActionResult> Enroll(int courseId)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _enrollmentService.EnrollAsync(userId, courseId);
        return Ok(result);
    }

    [HttpGet("my-enrollments")]
    public async Task<IActionResult> GetMyEnrollments()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _enrollmentService.GetUserEnrollmentsAsync(userId);
        return Ok(result);
    }

    [HttpGet("{enrollmentId}")]
    public async Task<IActionResult> GetById(int enrollmentId)
    {
        var result = await _enrollmentService.GetEnrollmentByIdAsync(enrollmentId);
        return Ok(result);
    }

    [HttpPut("{enrollmentId}/progress")]
    public async Task<IActionResult> UpdateProgress(int enrollmentId, [FromBody] UpdateProgressDto dto)
    {
        await _enrollmentService.UpdateProgressAsync(enrollmentId, dto);
        return NoContent();
    }

    [HttpGet("check/{courseId}")]
    public async Task<IActionResult> CheckEnrollment(int courseId)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _enrollmentService.IsUserEnrolledAsync(userId, courseId);
        return Ok(new { isEnrolled = result });
    }
}
