using PetSim.Server.Models;
using PetSim.Server.Repositories;
namespace PetSim.Server.Services;

public class StatsService
{

    private readonly PetStatsRepository _statsRepository;

    public StatsService(PetStatsRepository statsRepository)
    {
        _statsRepository = statsRepository;
    }

    public async Task<List<PetStats>> GetAllStats()
    {
        return await _statsRepository.GetAllStats();
    }

    public async Task<PetStats?> GetStatsByPetId(Guid petId)
    {
        return await _statsRepository.GetStatsByPetId(petId);
    }

    public async Task<PetStats?> UpdateStatsByPetId(Guid petId, UpdateStatsDto statsUpdate)
    {
        return await _statsRepository.UpdateStatsByPetId(petId, statsUpdate);
    }

}