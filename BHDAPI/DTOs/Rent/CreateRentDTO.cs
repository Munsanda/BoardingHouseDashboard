public class CreateRentDTO
{
    public int StudentId { get; set; } // Foreign Key to Student
    public decimal Amount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

}
