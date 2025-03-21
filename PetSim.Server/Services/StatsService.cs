using PetSim.Server.Models;
using PetSim.Server.Repositories;
namespace PetSim.Server.Services;

public class StatsService {

    private readonly StatsRepository _statsRepository;

    public StatsService(StatsRepository statsRepository) {
        _statsRepository = statsRepository;
    }

    public async Task<List<Stats>> GetAllStats()
    {
        return await _statsRepository.GetAllStats();
    }

    public async Task<Stats?> GetStatsByPetId(Guid petId)
    {
        return await _statsRepository.GetStatsByPetId(petId);
    }

    public async Task<Stats?> UpdateStatsByPetId(Guid petId, UpdateStatsDto statsUpdate)
    {
        return await _statsRepository.UpdateStatsByPetId(petId, statsUpdate);
    }

}