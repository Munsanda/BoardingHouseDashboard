public class Rent
{
    public int Id { get; set; } // Primary Key
    public int StudentId { get; set; } // Foreign Key to Student
    public decimal Amount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    // Navigation Property
    public Student Student { get; set; }
}
