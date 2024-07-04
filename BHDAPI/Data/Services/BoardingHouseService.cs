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
        return await _context.Rooms.
            Where(r => r.BoardingHouseId == id).
            Include(p => p.Repairs).
            Include(s => s.Students).
            ThenInclude(t => t.Rents)
            .Select(r => new Room
            {
                Id = r.Id,
                Name = r.Name,
                BoardingHouseId = r.BoardingHouseId,
                Students = r.Students.Select(s => new Student
                {
                    Id = s.Id,
                    Fname = s.Fname,
                    Lname = s.Lname,
                    IdNumber = s.IdNumber,
                    RoomId = s.RoomId,
                    DateOfEntry = s.DateOfEntry,
                    NumberOfWarnings = s.NumberOfWarnings,
                    Rents = s.Rents.Select(t => new Rent
                    {
                        Id = t.Id,
                        StartDate = t.StartDate,
                        EndDate = t.EndDate,
                        Amount = t.Amount
                    }).ToList()
                }).ToList(),
                Repairs = r.Repairs.Select(p => new Repair
                {
                    Id = p.Id,
                    DateOfReport = p.DateOfReport,
                    DateOfCompletion = p.DateOfCompletion,
                    Cost = p.Cost,
                    Notes = p.Notes,
                    RoomId = p.RoomId,
                    RepairsComplete = p.RepairsComplete
                }).ToList()
            }).ToListAsync();

    }

     
}
