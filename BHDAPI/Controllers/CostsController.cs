using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using BHDAPI.Data.Interfaces;
using BHDAPI;

namespace BHDAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CostController : ControllerBase
    {
        private readonly ICostService _costService;

        public CostController(ICostService costService)
        {
            _costService = costService;
        }

        // GET: api/cost/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CostDTO>> GetCostById(int id)
        {
            var cost = await _costService.GetCostByIdAsync(id);
            if (cost == null)
            {
                return NotFound();
            }
            return Ok(cost.AsDTO());
        }

        // GET: api/cost
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CostDTO>>> GetAllCosts()
        {
            var costs = await _costService.GetAllCostsAsync();
            return Ok(costs.Select(x => x.AsDTO()).ToList());
        }

        // POST: api/cost
        [HttpPost]
        public async Task<ActionResult> AddCost([FromBody] CreateCostDTO cost)
        {
            if (cost == null)
            {
                return BadRequest("Cost cannot be null.");
            }

            Cost newCost = new Cost(){
                Amount = cost.Amount,
                Description = cost.Description,
                Date = cost.Date,
                Type = cost.Type,
                Category = cost.Category,
                BoardingHouseId = cost.BoardingHouseId,
                rentId = cost.RentId,
                repairId = cost.RepairId
            };

            await _costService.AddCostAsync(newCost);
            return CreatedAtAction(nameof(GetCostById), new { id = newCost.Id }, newCost.AsDTO());
        }

        // PUT: api/cost/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCost(int id, [FromBody] Cost cost)
        {
            if (id != cost.Id)
            {
                return BadRequest("Cost ID mismatch.");
            }

            var existingCost = await _costService.GetCostByIdAsync(id);
            if (existingCost == null)
            {
                return NotFound();
            }

            await _costService.UpdateCostAsync(cost);
            return NoContent();
        }

        // DELETE: api/cost/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCost(int id)
        {
            var cost = await _costService.GetCostByIdAsync(id);
            if (cost == null)
            {
                return NotFound();
            }

            await _costService.DeleteCostAsync(id);
            return NoContent();
        }

        // GET: api/cost/byType/{type}
        [HttpGet("byType/{type}")]
        public async Task<ActionResult<IEnumerable<CostDTO>>> GetCostsByType(CostType type)
        {
            var costs = await _costService.GetCostsByTypeAsync(type);
            return Ok(costs.Select(x => x.AsDTO()).ToList());
        }

        // GET: api/cost/byCategory/{category}
        [HttpGet("byCategory/{category}")]
        public async Task<ActionResult<IEnumerable<CostDTO>>> GetCostsByCategory(CostCategory category)
        {
            var costs = await _costService.GetCostsByCategoryAsync(category);
            return Ok(costs.Select(x => x.AsDTO()).ToList());
        }

        // GET: api/cost/byDate/{date}
        [HttpGet("byDate/{date}")]
        public async Task<ActionResult<IEnumerable<CostDTO>>> GetCostsByDate(DateTime date)
        {
            var costs = await _costService.GetCostsByDateAsync(date);
            return Ok(costs.Select(x => x.AsDTO()).ToList());
        }

        // GET: api/cost/byAmount/{amount}
        [HttpGet("byAmount/{amount}")]
        public async Task<ActionResult<IEnumerable<Cost>>> GetCostsByAmount(decimal amount)
        {
            var costs = await _costService.GetCostsByAmountAsync(amount);
            return Ok(costs.Select(x => x.AsDTO()).ToList());
        }

        // GET: api/cost/byAmountRange
        [HttpGet("byAmountRange")]
        public async Task<ActionResult<IEnumerable<CostDTO>>> GetCostsByAmountRange(decimal minAmount, decimal maxAmount)
        {
            var costs = await _costService.GetCostsByAmountRangeAsync(minAmount, maxAmount);
            return Ok(costs.Select(x => x.AsDTO()).ToList());
        }

        // GET: api/cost/categories
        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<CostCategory>>> GetAllCostCategories()
        {
            var categories = await _costService.GetAllCostCategoriesAsync();
            return Ok(categories);
        }

        // GET: api/cost/monthlySummary
        [HttpGet("monthlySummary")]
        public async Task<ActionResult<decimal>> GetMonthlySummary(int month, int year)
        {
            var summary = await _costService.GetMonthlySummaryAsync(month, year);
            return Ok(summary);
        }

        // GET: api/cost/yearlySummary
        [HttpGet("yearlySummary")]
        public async Task<ActionResult<decimal>> GetYearlySummary(int year)
        {
            var summary = await _costService.GetYearlySummaryAsync(year);
            return Ok(summary);
        }
    }
}