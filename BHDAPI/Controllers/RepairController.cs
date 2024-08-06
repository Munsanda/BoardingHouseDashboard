using BHDAPI.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RepairController : ControllerBase
{
    private readonly IRepairService _repairService;
    private readonly IRoomService _roomService;
    private readonly ICostService _costService;

    public RepairController(IRepairService repairService, IRoomService roomService, ICostService costService)
    {
        _repairService = repairService;
        _roomService = roomService;
        _costService = costService;
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

        //Get room
        if( repair.RepairsComplete == false){
            Cost createCost = new Cost{
                Amount = Urepair.Cost,
                Description = "Repair paid for: " + Urepair.Notes + " " + DateTime.Now /*Date of completion*/,
                Date = (DateTime) repair.DateOfCompletion,
                Type = CostType.Expense,
                Category = CostCategory.Maintenance,
                BoardingHouseId = repair.Room.BoardingHouseId,
                repairId = repair.Id,
            };
            await _costService.AddCostAsync(createCost);
        }

        repair.Cost = Urepair.Cost;
        repair.Notes = Urepair.Notes;
        repair.RepairsComplete = true;
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
