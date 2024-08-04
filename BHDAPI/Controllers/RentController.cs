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

    [HttpGet("overview")]
    public async Task<IActionResult> GetOverview()
    {
        try
        {
            var month = DateTime.Now;

            var totalCollected = await _rentService.GetTotalRentCollectedAsync(month);
            var outstandingPayments = await _rentService.GetOutstandingPaymentsAsync(month);
            var expectedIncome = await _rentService.GetExpectedIncomeAsync(month);

            return Ok(new
            {
                TotalCollected = totalCollected,
                OutstandingPayments = outstandingPayments,
                ExpectedIncome = expectedIncome
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("quick-stats")]
    public async Task<IActionResult> GetQuickStats()
    {
        try
        {
            var occupiedRooms = await _rentService.GetOccupiedRoomsCountAsync();
            var totalRooms = await _rentService.GetTotalRoomsCountAsync();
            var averageRent = await _rentService.GetAverageRentAsync();
            var historicalTrends = await _rentService.GetHistoricalIncomeTrendsAsync();

            var occupancyRate = (double)occupiedRooms / totalRooms * 100;

            return Ok(new
            {
                OccupancyRate = occupancyRate,
                AverageRent = averageRent,
                HistoricalTrends = historicalTrends
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

}