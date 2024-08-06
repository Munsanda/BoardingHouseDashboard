using BHDAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

public class StudentService : IStudentService
{
    private readonly BoardingHouseContext _context;

    public StudentService(BoardingHouseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Student>> GetAllStudentsAsync()
    {
        return await _context.Students.ToListAsync();
    }

    public async Task<Student> GetStudentByIdAsync(int id)
    {
        return await _context.Students
                         .Include(r => r.Room) // Assuming 'Room' is the navigation property
                         .FirstOrDefaultAsync(r => r.Id == id); // Use FirstOrDefaultAsync instead of FindAsync
    }

    public async Task<Student> CreateStudentAsync(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return student;
    }

    public async Task<Student> UpdateStudentAsync(Student student)
    {
        _context.Entry(student).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return student;
    }

    public async Task DeleteStudentAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            throw new KeyNotFoundException("Student not found");
        }

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Rent>> GetRentsByStudentIdAsync(int id)
    {
        return await _context.Rents.Where(r => r.StudentId == id).ToListAsync();
    }

    public async Task<IEnumerable<Warning>> GetWarningsByStudentIdAsync(int id)
    {
        return await _context.Warnings.Where(w => w.StudentId == id).ToListAsync();
    }

    
}
