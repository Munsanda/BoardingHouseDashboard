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
        return await _context.Rents
                         .Include(r => r.Student) // Assuming 'Room' is the navigation property
                         .FirstOrDefaultAsync(r => r.Id == id); // Use FirstOrDefaultAsync instead of FindAsync
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

    public async Task<decimal> GetTotalRentCollectedAsync(DateTime month)
    {
        // Calculate total rent collected for the specified month
        return await _context.Rents
            .Where(r => r.StartDate.Month == month.Month && r.StartDate.Year == month.Year)
            .SumAsync(r => r.Amount);
    }

    public async Task<decimal> GetOutstandingPaymentsAsync(DateTime month)
    {
        // Calculate total outstanding payments
        return await _context.Rents
            .Where(r => r.EndDate < DateTime.Now)
            .SumAsync(r => r.Amount);
    }

    public async Task<decimal> GetExpectedIncomeAsync(DateTime month)
    {
        // Calculate expected income for the month
        return await _context.Rents
            .Where(r => r.StartDate.Month == month.Month && r.StartDate.Year == month.Year)
            .SumAsync(r => r.Amount);
    }

    public async Task<int> GetOccupiedRoomsCountAsync()
    {
        // Calculate the number of occupied rooms
        return await _context.Rooms
            .CountAsync(r => r.Students.Count() == 3);
    }

    public async Task<int> GetTotalRoomsCountAsync()
    {
        // Calculate the total number of rooms
        return await _context.Rooms.CountAsync();
    }

    public async Task<decimal> GetAverageRentAsync()
    {
        // Calculate the average rent
        return await _context.Rents
            .AverageAsync(r => r.Amount);
    }

    public async Task<IEnumerable<MonthlyIncome>> GetHistoricalIncomeTrendsAsync()
    {
        // Calculate historical income trends
        return await _context.Rents
            .GroupBy(r => new { r.StartDate.Year, r.StartDate.Month })
            .Select(g => new MonthlyIncome
            {
                Month = new DateTime(g.Key.Year, g.Key.Month, 1),
                TotalIncome = g.Sum(r => r.Amount)
            })
            .ToListAsync();
    }
}