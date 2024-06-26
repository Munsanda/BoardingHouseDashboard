using BHDAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
public class RentService : IRentService
{
    private readonly BoardingHouseContext _context;

    public RentService(BoardingHouseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Rent>> GetAllRentsAsync()
    {
        return await _context.Rents.ToListAsync();
    }

    public async Task<Rent> GetRentByIdAsync(int id)
    {
        return await _context.Rents.FindAsync(id);
    }

    public async Task<Rent> CreateRentAsync(Rent rent)
    {
        _context.Rents.Add(rent);
        await _context.SaveChangesAsync();
        return rent;
    }

    public async Task<Rent> UpdateRentAsync(Rent rent)
    {
        _context.Entry(rent).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return rent;
    }

    public async Task DeleteRentAsync(int id)
    {
        var rent = await _context.Rents.FindAsync(id);
        if (rent == null)
        {
            throw new KeyNotFoundException("Rent not found");
        }

        _context.Rents.Remove(rent);
        await _context.SaveChangesAsync();
    }

    
}