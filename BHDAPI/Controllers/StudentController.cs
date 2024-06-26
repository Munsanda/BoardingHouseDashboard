using BHDAPI.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly IRoomService _roomService;

    public StudentController(IStudentService studentService, IRoomService roomService)
    {
        _studentService = studentService;
        _roomService = roomService;
    }

    [HttpPost("/api/rooms/{id}/students")]
    public async Task<IActionResult> CreateStudentForRoom(int id, Student student)
    {
        var room = await _roomService.GetRoomByIdAsync(id);
        if (room == null)
        {
            return NotFound();
        }
        student.RoomId = id;
        var createdStudent = await _studentService.CreateStudentAsync(student);
        return CreatedAtAction("GetStudentsByRoomId", "Room", new { id = room.Id }, createdStudent);
    }

    [HttpGet("{id}/rents")]
    public async Task<IActionResult> GetRentsByStudentId(int id)
    {
        var student = await _studentService.GetStudentByIdAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        return Ok(await _studentService.GetRentsByStudentIdAsync(id));
    }

    [HttpGet("{id}/warnings")]
    public async Task<IActionResult> GetWarningsByStudentId(int id)
    {
        var student = await _studentService.GetStudentByIdAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        return Ok(await _studentService.GetWarningsByStudentIdAsync(id));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        try
        {
            await _studentService.DeleteStudentAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

}
