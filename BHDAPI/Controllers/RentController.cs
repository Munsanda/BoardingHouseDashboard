using BHDAPI.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class RentController : ControllerBase
{
    private readonly IRentService _rentService;
    private readonly IStudentService _studentService;

    public RentController(IRentService rentService, IStudentService studentService)
    {
        _rentService = rentService;
        _studentService = studentService;
    }

    [HttpPost("/api/students/{_studentId}/rent")]
    public async Task<IActionResult> CreateRentForStudent(int _studentId, CreateRentDTO rent)
    {
        var student = await _studentService.GetStudentByIdAsync(_studentId);
        
        if (student == null)
        {
            return NotFound();
        }
        rent.StudentId = _studentId;
        Rent newRent = new(){
            StudentId = _studentId,
            Amount = rent.Amount,
            StartDate = rent.StartDate,
            EndDate = rent.EndDate
        };
        var createdRent = await _rentService.CreateRentAsync(newRent);
        return CreatedAtAction(nameof(GetRentById), new { id = createdRent.Id }, createdRent);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRentById(int id)
    {
        var rent = await _rentService.GetRentByIdAsync(id);
        if (rent == null)
        {
            return NotFound();
        }
        return Ok(rent);
    }

}