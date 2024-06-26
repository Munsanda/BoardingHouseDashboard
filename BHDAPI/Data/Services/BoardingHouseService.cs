using BHDAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

public class BoardingHouseService : IBoardingHouseService
{
    private readonly BoardingHouseContext _context;

    public BoardingHouseService(BoardingHouseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BoardingHouse>> GetAllBoardingHousesAsync()
    {
        return await _context.BoardingHouses.ToListAsync();
    }

    public async Task<BoardingHouse> GetBoardingHouseByIdAsync(int id)
    {
        return await _context.BoardingHouses.FindAsync(id);
    }

    public async Task<BoardingHouse> CreateBoardingHouseAsync(BoardingHouse boardingHouse)
    {
        _context.BoardingHouses.Add(boardingHouse);
        await _context.SaveChangesAsync();
        return boardingHouse;
    }

    public async Task<BoardingHouse> UpdateBoardingHouseAsync(BoardingHouse boardingHouse)
    {
        _context.Entry(boardingHouse).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return boardingHouse;
    }

    public async Task DeleteBoardingHouseAsync(int id)
    {
        var boardingHouse = await _context.BoardingHouses.FindAsync(id);
        if (boardingHouse == null)
        {
            throw new KeyNotFoundException("Boarding house not found");
        }

        _context.BoardingHouses.Remove(boardingHouse);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Room>> GetRoomsByBoardingHouseIdAsync(int id)  // Implement this method
    {
        return await _context.Rooms.Where(r => r.BoardingHouseId == id).ToListAsync();
    }

    
}
