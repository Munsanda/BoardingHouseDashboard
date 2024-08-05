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
    public async Task<IActionResult> ReportRepairForRoom(int id, CreateRepairDTO repair)
    {
        var room = await _roomService.GetRoomByIdAsync(id);
        
        if (room == null)
        {
            return NotFound();
        }
        repair.RoomId = id;
        Repair newRepair = new(){
            DateOfReport = repair.DateOfReport,
            DateOfCompletion = repair.DateOfCompletion,
            Notes = repair.Notes,
            RepairsComplete = false,
            RoomId = repair.RoomId
        };
        var createdRepair = await _repairService.CreateRepairAsync(newRepair);
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

    [HttpGet("/api/rooms/{id}/repairs")]
    public async Task<IActionResult> getRepairsByRoomId(int id)
    {
        //var room = await _roomService.GetRoomByIdAsync(id);
        var repairs = await _repairService.GetRepairsByRoomIdAsync(id);
        if (repairs == null)
        {
            return NotFound();
        }
        return Ok(repairs);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRepair(int id, UpdateRepairDTO Urepair)
    {


        var repair = await _repairService.GetRepairByIdAsync(id);
        repair.Cost = Urepair.Cost;
        repair.RepairsComplete = true;
        repair.Notes = Urepair.Notes;
        repair.DateOfCompletion = DateTime.Now;

        var updatedRepair = await _repairService.UpdateRepairAsync(repair);
        return CreatedAtAction(nameof(GetRepairById), new { id = updatedRepair.Id }, updatedRepair);
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
