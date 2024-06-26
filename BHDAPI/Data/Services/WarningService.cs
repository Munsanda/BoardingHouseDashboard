using BHDAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

public class WarningService : IWarningService
{
    private readonly BoardingHouseContext _context;

    public WarningService(BoardingHouseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Warning>> GetAllWarningsAsync()
    {
        return await _context.Warnings.ToListAsync();
    }

    public async Task<Warning> GetWarningByIdAsync(int id)
    {
        return await _context.Warnings.FindAsync(id);
    }

    public async Task<Warning> CreateWarningAsync(Warning warning)
    {
        _context.Warnings.Add(warning);
        await _context.SaveChangesAsync();
        return warning;
    }

    public async Task<Warning> UpdateWarningAsync(Warning warning)
    {
        _context.Entry(warning).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return warning;
    }

    public async Task DeleteWarningAsync(int id)
    {
        var warning = await _context.Warnings.FindAsync(id);
        if (warning == null)
        {
            throw new KeyNotFoundException("Warning not found");
        }

        _context.Warnings.Remove(warning);
        await _context.SaveChangesAsync();
    }
    
}
