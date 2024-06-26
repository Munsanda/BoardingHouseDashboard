public class Repair
{
    public int Id { get; set; } // Primary Key
    public DateTime DateOfReport { get; set; }
    public string Notes { get; set; }
    public bool RepairsComplete { get; set; }
    public decimal Cost { get; set; }
    public DateTime? DateOfCompletion { get; set; }
    public int RoomId { get; set; } // Foreign Key to Room

    // Navigation Property
    public Room Room { get; set; }
}
