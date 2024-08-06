// DTO for updating a Cost
public class UpdateCostDTO
{
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public CostType Type { get; set; }
    public CostCategory Category { get; set; }
}