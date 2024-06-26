using BHDAPI.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RepairController : ControllerBase
{
    private readonly IRepairService _repairService;
    private readonly IRoomService _roomService;

    public RepairController(IRepairService repairService, IRoomService roomService)
    {
        _repairService = repairService;
        _roomService = roomService;
    }

    [HttpPost("/api/rooms/{id}/repairs")]
    public async Task<IActionResult> ReportRepairForRoom(int id, Repair repair)
    {
        var room = await _roomService.GetRoomByIdAsync(id);
        if (room == null)
        {
            return NotFound();
        }
        repair.RoomId = id;
        var createdRepair = await _repairService.CreateRepairAsync(repair);
        return CreatedAtAction(nameof(GetRepairById), new { id = createdRepair.Id }, createdRepair);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRepairById(int id)
    {
        var repair = await _repairService.GetRepairByIdAsync(id);
        if (repair == null)
        {
            return NotFound();
        }
        return Ok(repair);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRepair(int id)
    {
        try
        {
            await _repairService.DeleteRepairAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

}
