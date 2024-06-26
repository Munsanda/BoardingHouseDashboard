using BHDAPI.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BoardingHouseController : ControllerBase
{
    private readonly IBoardingHouseService _boardingHouseService;

    public BoardingHouseController(IBoardingHouseService boardingHouseService)
    {
        _boardingHouseService = boardingHouseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBoardingHouseByName(string name)
    {
        var boardingHouses = await _boardingHouseService.GetAllBoardingHousesAsync();
        var filteredHouses = boardingHouses.Where(b => b.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        return Ok(filteredHouses);
    }

    [HttpGet("{id}/rooms")]
    public async Task<IActionResult> GetRoomsByBoardingHouseId(int id)
    {
        var boardingHouse = await _boardingHouseService.GetBoardingHouseByIdAsync(id);
        if (boardingHouse == null)
        {
            return NotFound();
        }
        return Ok(boardingHouse.Rooms);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBoardingHouse(BoardingHouse boardingHouse)
    {
        var createdBoardingHouse = await _boardingHouseService.CreateBoardingHouseAsync(boardingHouse);
        return CreatedAtAction(nameof(GetBoardingHouseByName), new { name = createdBoardingHouse.Name }, createdBoardingHouse);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBoardingHouse(int id)
    {
        try
        {
            await _boardingHouseService.DeleteBoardingHouseAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

}
