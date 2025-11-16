using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCourseApi.Core.Interfaces;

namespace OnlineCourseApi.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class SectionController : ControllerBase
{
    private readonly ISectionService _sectionService;

    public SectionController(ISectionService sectionService)
    {
        _sectionService = sectionService;
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _sectionService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("course/{courseId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByCourseId(int courseId)
    {
        var result = await _sectionService.GetByCourseIdAsync(courseId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromQuery] int courseId, [FromQuery] string title, [FromQuery] int orderIndex)
    {
        var result = await _sectionService.CreateAsync(courseId, title, orderIndex);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _sectionService.DeleteAsync(id);
        return NoContent();
    }
}
