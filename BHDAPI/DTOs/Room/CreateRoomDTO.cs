public class CreateRoomDTO
{
    public int Id { get; set; } // Primary Key
    public string Name {get; set;}
    public int BoardingHouseId { get; set; } // Foreign Key to BoardingHouse

}
