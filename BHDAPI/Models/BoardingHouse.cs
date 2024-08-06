public class BoardingHouse
{
    public int Id { get; set; } // Primary Key
    public string Name { get; set; }
    public string Location { get; set; }

    // Navigation Property
    public ICollection<Room> Rooms { get; set; }
    public ICollection<Cost> Costs { get; set; }
}
