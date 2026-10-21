using Microsoft.AspNetCore.Mvc;

namespace EduHubApi.Controllers;

public record CreateRegistrationDto(int MemberId, int CourseId);

[ApiController]
[Route("api/v1/[controller]")]
public class RegistrationsController : ControllerBase
{
    private static readonly List<int> ExistingIds = new() { 42 }; // giả lập id 42 đã tồn tại

    // POST /api/v1/registrations
    [HttpPost]
    public IActionResult Create([FromBody] CreateRegistrationDto dto)
    {
        var newId = new Random().Next(100, 999);
        var result = new { id = newId, memberId = dto.MemberId, courseId = dto.CourseId, status = "pending" };
        return Created($"/api/v1/registrations/{newId}", result);
    }

    // DELETE /api/v1/registrations/42
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!ExistingIds.Contains(id))
        {
            return NotFound(new
            {
                type = "https://eduhub.example/errors/registration-not-found",
                title = "Không tìm thấy phiếu đăng ký",
                status = 404,
                detail = $"Không có phiếu đăng ký số {id}."
            });
        }
        ExistingIds.Remove(id); // xoá khỏi danh sách "tồn tại" để lần sau test lại ra 404
        return NoContent(); // 204
    }
}