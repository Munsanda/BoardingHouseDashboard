using BHDAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

public class RoomService : IRoomService
{
    private readonly BoardingHouseContext _context;

    public RoomService(BoardingHouseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Room>> GetAllRoomsAsync()
    {
        return await _context.Rooms.ToListAsync();
    }

    public async Task<Room> GetRoomByIdAsync(int id)
    {
        return await _context.Rooms.FindAsync(id);
    }

    public async Task<Room> CreateRoomAsync(Room room)
    {
        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();
        return room;
    }

    public async Task<Room> UpdateRoomAsync(Room room)
    {
        _context.Entry(room).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return room;
    }

    public async Task DeleteRoomAsync(int id)
    {
        var room = await _context.Rooms.FindAsync(id);
        if (room == null)
        {
            throw new KeyNotFoundException("Room not found");
        }

        _context.Rooms.Remove(room);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Student>> GetStudentsByRoomIdAsync(int id)  // Implement this method
    {
        return await _context.Students.Where(s => s.RoomId == id).ToListAsync();
    }

    
}

