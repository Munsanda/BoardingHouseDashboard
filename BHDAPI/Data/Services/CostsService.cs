using BHDAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;


public class CostService : ICostService
{
    private readonly BoardingHouseContext _context;

    public CostService(BoardingHouseContext context)
    {
        _context = context;
    }

    // CRUD operations

    public async Task<Cost> GetCostByIdAsync(int id)
    {
        return await _context.Costs.FindAsync(id);
    }

    public async Task<Cost> GetCostByRepairIdAsync(int id)
    {
        return await _context.Costs.FirstAsync(x => x.repairId == id);
    }

    public async Task<IEnumerable<Cost>> GetAllCostsAsync(int id)
    {
        return await _context.Costs.Where(x => x.BoardingHouseId == id).ToListAsync();
    }

    public async Task AddCostAsync(Cost cost)
    {
        _context.Costs.Add(cost);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCostAsync(Cost cost)
    {
        _context.Costs.Update(cost);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCostAsync(int id)
    {
        var cost = await _context.Costs.FindAsync(id);
        if (cost != null)
        {
            _context.Costs.Remove(cost);
            await _context.SaveChangesAsync();
        }
    }

    // Filter methods

    public async Task<IEnumerable<Cost>> GetCostsByTypeAsync(CostType type)
    {
        return await _context.Costs.Where(c => c.Type == type).ToListAsync();
    }

    public async Task<IEnumerable<Cost>> GetCostsByCategoryAsync(CostCategory category)
    {
        return await _context.Costs.Where(c => c.Category == category).ToListAsync();
    }

    public async Task<IEnumerable<Cost>> GetCostsByDateAsync(DateTime date)
    {
        return await _context.Costs.Where(c => c.Date.Date == date.Date).ToListAsync();
    }

    public async Task<IEnumerable<Cost>> GetCostsByAmountAsync(decimal amount)
    {
        return await _context.Costs.Where(c => c.Amount == amount).ToListAsync();
    }

    public async Task<IEnumerable<Cost>> GetCostsByAmountRangeAsync(decimal minAmount, decimal maxAmount)
    {
        return await _context.Costs.Where(c => c.Amount >= minAmount && c.Amount <= maxAmount).ToListAsync();
    }

    public async Task<IEnumerable<CostCategory>> GetAllCostCategoriesAsync()
    {
        // Assuming CostCategory is an enum or you have a separate entity for it
        return await _context.Costs
            .Select(c => c.Category)
            .Distinct()
            .ToListAsync();
    }

     // Financial summaries

    public async Task<decimal> GetMonthlySummaryAsync(int month, int year)
    {
        return await _context.Costs
            .Where(c => c.Date.Month == month && c.Date.Year == year)
            .SumAsync(c => c.Amount);
    }

    public async Task<decimal> GetYearlySummaryAsync(int year)
    {
        return await _context.Costs
            .Where(c => c.Date.Year == year)
            .SumAsync(c => c.Amount);
    }
}
