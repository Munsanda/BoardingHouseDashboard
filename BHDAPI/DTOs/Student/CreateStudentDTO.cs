public class CreateStudentDTO
{
    public int Id { get; set; } // Primary Key
    public string Fname { get; set; }
    public string Lname { get; set; }
    public string IdNumber { get; set; }
    public int RoomId { get; set; } // Foreign Key to Room
    public DateTime DateOfEntry { get; set; }

}