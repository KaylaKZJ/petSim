using PetSim.Server.Models;
using PetSim.Server.Repositories;
namespace PetSim.Server.Services;

public class StatsActionService
{

    private readonly StatsActionRepository _repository;

    public StatsActionService(StatsActionRepository repository)
    {
        _repository = repository;
    }


    public async Task<StatsAction> CreateStatsAction(CreateStatsActionDto createStatsActionDto)
    {
        return await _repository.CreateStatsAction(createStatsActionDto);
    }

    public async Task<StatsAction?> GetStatsAction(string actionType)
    {
        return await _repository.GetStatsAction(actionType);
    }

    public async Task<StatsAction?> UpdateStatsAction(string actionType, UpdateStatsActionDto updateStatsActionDto)
    {
        return await _repository.UpdateStatsAction(actionType, updateStatsActionDto);
    }


    public async Task<bool> DeleteStatsAction(string actionType)
    {
        return await _repository.DeleteStatsAction(actionType);
    }

}