// DTO for Cost
public class CostDTO
{
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public CostType Type { get; set; }
    public CostCategory Category { get; set; }
    public int BoardingHouseId { get; set; }
    public int RentId { get; set; }
    public int RepairId { get; set; }
}