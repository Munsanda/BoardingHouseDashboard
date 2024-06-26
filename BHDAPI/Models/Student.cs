public class Student
{
    public int Id { get; set; } // Primary Key
    public string Fname { get; set; }
    public string Lname { get; set; }
    public string IdNumber { get; set; }
    public int RoomId { get; set; } // Foreign Key to Room
    public DateTime DateOfEntry { get; set; }
    public int NumberOfWarnings { get; set; }

    // Navigation Properties
    public Room Room { get; set; }
    public ICollection<Rent> Rents { get; set; }
    public ICollection<Warning> Warnings { get; set; }
}
