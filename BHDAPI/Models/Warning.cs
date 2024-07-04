public class Warning
{
    public int Id { get; set; } // Primary Key
    public int StudentId { get; set; } // Foreign Key to Student
    public string ReportType { get; set; }
    public DateTime ReportDate { get; set; }
    // Navigation Property
    public Student Student { get; set; }
}
