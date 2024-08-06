using Microsoft.EntityFrameworkCore;

public class BoardingHouseContext : DbContext
{
    public DbSet<BoardingHouse> BoardingHouses { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Rent> Rents { get; set; }
    public DbSet<Repair> Repairs { get; set; }
    public DbSet<Warning> Warnings { get; set; }
    public DbSet<Cost> Costs {get; set;}

    public BoardingHouseContext(DbContextOptions<BoardingHouseContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BoardingHouse>()
            .HasMany(b => b.Rooms)
            .WithOne(r => r.BoardingHouse)
            .HasForeignKey(r => r.BoardingHouseId);

        modelBuilder.Entity<BoardingHouse>()
            .HasMany(b => b.Rooms)
            .WithOne(r => r.BoardingHouse)
            .HasForeignKey(r => r.BoardingHouseId);

        modelBuilder.Entity<BoardingHouse>()
            .HasMany(bh => bh.Costs)
            .WithOne(e => e.BoardingHouse)
            .HasForeignKey(r => r.BoardingHouseId);

        modelBuilder.Entity<Room>()
            .HasMany(r => r.Students)
            .WithOne(s => s.Room)
            .HasForeignKey(s => s.RoomId);

        modelBuilder.Entity<Room>()
            .HasMany(r => r.Repairs)
            .WithOne(re => re.Room)
            .HasForeignKey(re => re.RoomId);

        modelBuilder.Entity<Student>()
            .HasMany(s => s.Rents)
            .WithOne(r => r.Student)
            .HasForeignKey(r => r.StudentId);

        modelBuilder.Entity<Student>()
            .HasMany(s => s.Warnings)
            .WithOne(w => w.Student)
            .HasForeignKey(w => w.StudentId);

    }
}
