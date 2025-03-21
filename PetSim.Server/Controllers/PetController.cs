using Microsoft.AspNetCore.Mvc;
using PetSim.Server.Services;

namespace PetSim.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetController : ControllerBase {
        private readonly PetService _petService;

        public PetController(PetService petService) {
            _petService = petService;
        }


        [HttpPost] 
        public async Task<IActionResult> CreatePet(CreatePetDto CreatePetDto) {
           var newPetId = await _petService.CreatePet(CreatePetDto);
            return Ok(new { message = "Pet created successfully!", Id = newPetId });
        }

        [HttpGet]
        public async Task<IActionResult> GetPet(Guid id) {
            var pet = await _petService.GetPet(id);

            if (pet == null)
            {
                return NotFound(new { message = "Pet not found" });
            }

            pet.Stats = null;
            return Ok(new { message = "Pet found successfully!", content = pet });
        }

        [HttpPut] 
        public async Task<IActionResult> UpdatePet(Guid id, UpdatePetDto updatedPet) {
            var pet = await _petService.UpdatePet(id, updatedPet);

            if (pet == null)
            {
                return NotFound(new { message = "Pet not found" });
            }

            pet.Stats = null;
            return Ok(new { message = "Pet Updated successfully!", content = pet });

        }

        [HttpDelete] 
        public async Task<IActionResult> DeletePet(Guid id) {
            await _petService.DeletePet(id);
            return Ok(new { message = "Pet deleted successfully!" });
        }
    }
}