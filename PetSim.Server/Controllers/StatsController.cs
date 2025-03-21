using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PetSim.Server.Models;
using PetSim.Server.Services;

namespace PetSim.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatsController : ControllerBase {
        private readonly StatsService _statsService;

        public StatsController(StatsService statsService) {
            _statsService = statsService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllStats() {
            var stats = await _statsService.GetAllStats();

            if (stats == null)
            {
                return NotFound(new { message = "Stats not found" });
            }

            return Ok(new { message = "Stats found successfully!", content = stats });
        }

        [HttpGet("{petId}")]
        public async Task<IActionResult> GetStatsByPetId(Guid petId) {
            var stats = await _statsService.GetStatsByPetId(petId);

            if (stats == null)
            {
                return NotFound(new { message = "Stats not found" });
            }

            return Ok(new { message = "Stats found successfully!", content = stats });
        }

        [HttpPut] 
        public async Task<IActionResult> UpdateStats(Guid petId, UpdateStatsDto updatedStats) {
            var stats = await _statsService.UpdateStatsByPetId(petId, updatedStats);

            if (stats == null)
            {
                return NotFound(new { message = "Stats not found" });
            }

            return Ok(new { message = "Stats Updated successfully!", content = stats });

        }

    }
}