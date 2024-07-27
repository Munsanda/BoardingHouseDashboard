public class CreateRepairDTO
{
    public DateTime DateOfReport { get; set; }
    public string Notes { get; set; }
    public bool RepairsComplete { get; set; }
    public decimal Cost { get; set; }
    public DateTime? DateOfCompletion { get; set; }
    public int RoomId { get; set; } // Foreign Key to Room

}