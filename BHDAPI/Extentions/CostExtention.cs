namespace BHDAPI {
    public static class CostExtention {
        public static CostDTO AsDTO(this Cost cost){
            if (cost == null) return null;
            return new CostDTO{
                Amount = cost.Amount,
                Description = cost.Description,
                Date = cost.Date,
                Type = cost.Type,
                Category = cost.Category,
                BoardingHouseId = cost.BoardingHouseId,
                RentId = (int)cost.rentId,
                RepairId = (int)cost.repairId
            };
        }

        public static Cost AsCost(this CostDTO costDTO){
            if (costDTO == null) return null;
            return new Cost {
                Amount = costDTO.Amount,
                Description = costDTO.Description,
                Date = costDTO.Date,
                Type = costDTO.Type,
                Category = costDTO.Category,
                BoardingHouseId = costDTO.BoardingHouseId,
                rentId = costDTO.RentId,
                repairId = costDTO.RepairId 
            };
        }
    }
}