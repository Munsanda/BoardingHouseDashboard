using BHDAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
public class RepairService : IRepairService
{
    private readonly BoardingHouseContext _context;

    public RepairService(BoardingHouseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Repair>> GetAllRepairsAsync()
    {
        return await _context.Repairs.ToListAsync();
    }

    public async Task<Repair> GetRepairByIdAsync(int id)
    {
        return await _context.Repairs.FindAsync(id);
    }



    public async Task<Repair> CreateRepairAsync(Repair repair)
    {
        _context.Repairs.Add(repair);
        await _context.SaveChangesAsync();
        return repair;
    }

    public async Task<Repair> UpdateRepairAsync(Repair repair)
    {
        _context.Entry(repair).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return repair;
    }

    public async Task DeleteRepairAsync(int id)
    {
        var repair = await _context.Repairs.FindAsync(id);
        if (repair == null)
        {
            throw new KeyNotFoundException("Repair not found");
        }

        _context.Repairs.Remove(repair);
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<Repair>> GetRepairsByRoomIdAsync(int id)
    {
        return await _context.Repairs.Where(x => x.RoomId == id).ToListAsync();
    }
}
