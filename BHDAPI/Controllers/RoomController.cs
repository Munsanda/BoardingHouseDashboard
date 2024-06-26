using BHDAPI.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly IRoomService _roomService;
    private readonly IBoardingHouseService _boardingHouseService;

    public RoomController(IRoomService roomService, IBoardingHouseService boardingHouseService)
    {
        _roomService = roomService;
        _boardingHouseService = boardingHouseService;
    }

    [HttpPost("/api/boardinghouses/{id}/rooms")]
    public async Task<IActionResult> CreateRoomForBoardingHouse(int id, CreateRoomDTO room)
    {
        var boardingHouse = await _boardingHouseService.GetBoardingHouseByIdAsync(id);
        if (boardingHouse == null)
        {
            return NotFound();
        }
        room.BoardingHouseId = id;
        Room newRoom = new Room(){
            BoardingHouseId = room.BoardingHouseId,
            Name = room.Name
        };

        
        var createdRoom = await _roomService.CreateRoomAsync(newRoom);
        return CreatedAtAction(nameof(GetRoomsByBoardingHouseId), new { id = boardingHouse.Id }, createdRoom);
    }

    [HttpGet("/api/boardinghouses/{id}/rooms")]
    public async Task<IActionResult> GetRoomsByBoardingHouseId(int id)  // Ensure this method exists
    {
        var boardingHouse = await _boardingHouseService.GetBoardingHouseByIdAsync(id);
        if (boardingHouse == null)
        {
            return NotFound();
        }
        return Ok(await _boardingHouseService.GetRoomsByBoardingHouseIdAsync(id));
    }

    [HttpGet("{id}/students")]
    public async Task<IActionResult> GetStudentsByRoomId(int id)
    {
        var room = await _roomService.GetRoomByIdAsync(id);
        if (room == null)
        {
            return NotFound();
        }
        return Ok(await _roomService.GetStudentsByRoomIdAsync(id));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoom(int id)
    {
        try
        {
            await _roomService.DeleteRoomAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

}
