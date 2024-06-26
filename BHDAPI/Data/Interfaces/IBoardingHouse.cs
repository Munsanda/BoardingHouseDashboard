

namespace BHDAPI.Data.Interfaces {
    public interface IBoardingHouseService
    {
        Task<IEnumerable<BoardingHouse>> GetAllBoardingHousesAsync();
        Task<BoardingHouse> GetBoardingHouseByIdAsync(int id);
        Task<BoardingHouse> CreateBoardingHouseAsync(BoardingHouse boardingHouse);
        Task<BoardingHouse> UpdateBoardingHouseAsync(BoardingHouse boardingHouse);
        Task DeleteBoardingHouseAsync(int id);
        Task<IEnumerable<Room>> GetRoomsByBoardingHouseIdAsync(int id);  // Add this method
    }



    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAllRoomsAsync();
        Task<Room> GetRoomByIdAsync(int id);
        Task<Room> CreateRoomAsync(Room room);
        Task<Room> UpdateRoomAsync(Room room);
        Task DeleteRoomAsync(int id);
        Task<IEnumerable<Student>> GetStudentsByRoomIdAsync(int id);  // Add this method
    }



    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentByIdAsync(int id);
        Task<Student> CreateStudentAsync(Student student);
        Task<Student> UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(int id);
        Task<IEnumerable<Rent>> GetRentsByStudentIdAsync(int id);
        Task<IEnumerable<Warning>> GetWarningsByStudentIdAsync(int id);
    }



    public interface IRentService
    {
        Task<IEnumerable<Rent>> GetAllRentsAsync();
        Task<Rent> GetRentByIdAsync(int id);
        Task<Rent> CreateRentAsync(Rent rent);
        Task<Rent> UpdateRentAsync(Rent rent);
        Task DeleteRentAsync(int id);
    }


    public interface IRepairService
    {
        Task<IEnumerable<Repair>> GetAllRepairsAsync();
        Task<Repair> GetRepairByIdAsync(int id);
        Task<Repair> CreateRepairAsync(Repair repair);
        Task<Repair> UpdateRepairAsync(Repair repair);
        Task DeleteRepairAsync(int id);
    }


    public interface IWarningService
    {
        Task<IEnumerable<Warning>> GetAllWarningsAsync();
        Task<Warning> GetWarningByIdAsync(int id);
        Task<Warning> CreateWarningAsync(Warning warning);
        Task<Warning> UpdateWarningAsync(Warning warning);
        Task DeleteWarningAsync(int id);
    }


}