using Microsoft.AspNetCore.Mvc;
using PetSim.Server.Services;

namespace StatsActionSim.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatsActionController : ControllerBase
    {
        private readonly StatsActionService _statsActionService;

        public StatsActionController(StatsActionService statsActionService)
        {
            _statsActionService = statsActionService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateStatsAction(CreateStatsActionDto CreateStatsActionDto)
        {
            var newStatsActionId = await _statsActionService.CreateStatsAction(CreateStatsActionDto);
            return Ok(new { message = "StatsAction created successfully!", Id = newStatsActionId });
        }

        [HttpGet]
        public async Task<IActionResult> GetStatsAction(string actionType)
        {
            var statsAction = await _statsActionService.GetStatsAction(actionType);

            if (statsAction == null)
            {
                return NotFound(new { message = "StatsAction not found" });
            }

            return Ok(new { message = "StatsAction found successfully!", content = statsAction });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStatsAction(string actionType, UpdateStatsActionDto updateStatsActionDto)
        {
            var statsAction = await _statsActionService.UpdateStatsAction(actionType, updateStatsActionDto);

            if (statsAction == null)
            {
                return NotFound(new { message = "StatsAction not found" });
            }

            return Ok(new { message = "StatsAction Updated successfully!", content = statsAction });

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStatsAction(string actionType)
        {
            await _statsActionService.DeleteStatsAction(actionType);
            return Ok(new { message = "StatsAction deleted successfully!" });
        }
    }
}