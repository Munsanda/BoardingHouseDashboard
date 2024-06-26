public class Room
{
    public int Id { get; set; } // Primary Key
    public int BoardingHouseId { get; set; } // Foreign Key to BoardingHouse

    // Navigation Properties
    public BoardingHouse BoardingHouse { get; set; }
    public ICollection<Student> Students { get; set; }
    public ICollection<Repair> Repairs { get; set; }
}
