using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCourseApi.Core.DTOs.Request;
using OnlineCourseApi.Core.Interfaces;

namespace OnlineCourseApi.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class LectureController : ControllerBase
{
    private readonly ILectureService _lectureService;

    public LectureController(ILectureService lectureService)
    {
        _lectureService = lectureService;
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _lectureService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("section/{sectionId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetBySectionId(int sectionId)
    {
        var result = await _lectureService.GetBySectionIdAsync(sectionId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLectureDto dto)
    {
        var result = await _lectureService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _lectureService.DeleteAsync(id);
        return NoContent();
    }
}
