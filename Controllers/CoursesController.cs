using Microsoft.AspNetCore.Mvc;

namespace EduHubApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CoursesController : ControllerBase
{
    private static readonly List<object> Courses = new()
    {
        new { id = 5, title = "Lập trình Web", capacity = 40, registered = 37, hasSeats = true },
        new { id = 6, title = "Cơ sở dữ liệu", capacity = 30, registered = 30, hasSeats = false },
        new { id = 7, title = "Lập trình Back End", capacity = 25, registered = 10, hasSeats = true }
    };

    // GET /api/v1/courses/5
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var course = Courses.FirstOrDefault(c => ((dynamic)c).id == id);
        if (course is null)
        {
            return NotFound(new
            {
                type = "https://eduhub.example/errors/course-not-found",
                title = "Không tìm thấy khoá học",
                status = 404,
                detail = $"Không có khoá học số {id}."
            });
        }
        return Ok(course);
    }

    // GET /api/v1/courses?hasSeats=true&sort=title
    [HttpGet]
    public IActionResult GetAll([FromQuery] bool? hasSeats, [FromQuery] string? sort)
    {
        var result = Courses.AsEnumerable();
        if (hasSeats == true)
            result = result.Where(c => ((dynamic)c).hasSeats == true);
        if (sort == "title")
            result = result.OrderBy(c => ((dynamic)c).title);
        return Ok(result);
    }
}