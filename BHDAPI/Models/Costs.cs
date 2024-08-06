    public class Cost
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public CostType Type { get; set; }  // Income or Expense
        public CostCategory Category { get; set; }  // Specific category of the cost
        public int BoardingHouseId { get; set; } // Foreign Key to BoardingHouse
        public int? rentId { get; set; } // Foreign Key to Student
        public int? repairId {get; set;}


        public BoardingHouse BoardingHouse { get; set; }
        public Rent Rent { get; set; }
        public Repair Repair{get; set;}
    }

    public enum CostType
    {
        Income,
        Expense
    }

    public enum CostCategory
    {
        Rent,
        Salaries,
        CleaningMaterials,
        Utilities,
        Maintenance,
        FoodSupplies,
        CollateralPayment,
        OppositeEntry,
        Other
    }
