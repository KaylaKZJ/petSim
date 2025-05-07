using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetSim.Server.Data;
using PetSim.Server.Models;
namespace PetSim.Server.Repositories;

public class StatsActionRepository
{

    private readonly PetSimContext _context;
    private readonly IMapper _mapper;

    public StatsActionRepository(PetSimContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<StatsAction> CreateStatsAction(CreateStatsActionDto createStatsActionDto)
    {
        var petType = await _context.PetTypes.SingleOrDefaultAsync(pt => pt.Type == createStatsActionDto.PetType);

        if (petType == null) throw new Exception($"Pet type not found for {createStatsActionDto.PetType}. Create stats action failed.");

        var statsActionId = Guid.NewGuid();



        var statsAction = new StatsAction
        {
            StatsActionId = statsActionId,
            Type = createStatsActionDto.Type,
            PetTypeID = petType.Id,
        };

        var statsDistribution = new StatsDistribution
        {
            Id = Guid.NewGuid(),
            StatsActionId = statsActionId,
            StatsAction = statsAction,
            Happiness = createStatsActionDto.StatsDistribution.Happiness,
            Hunger = createStatsActionDto.StatsDistribution.Hunger,
            Boredom = createStatsActionDto.StatsDistribution.Boredom
        };

        statsAction.StatsDistribution = statsDistribution;

        _context.StatsAction.Add(statsAction);
        await _context.SaveChangesAsync();

        return statsAction;
    }

    public async Task<StatsAction?> GetStatsAction(string actionType)
    {
        return await _context.StatsAction.FirstOrDefaultAsync(sa => sa.Type == actionType);
    }

    public async Task<StatsDistribution?> GetStatsDistribution(string actionType)
    {
        return await _context.StatsDistribution.Include(sd => sd.StatsAction).Where(sd => sd.StatsAction.Type == actionType).FirstOrDefaultAsync();
    }

    public async Task<StatsAction?> UpdateStatsAction(string actionType, UpdateStatsActionDto updateStatsActionDto)
    {
        var statsAction = await GetStatsAction(actionType);

        if (statsAction == null)
        {
            throw new Exception($"Stats action type {actionType} not found. Failed to update stats action");
        }


        _mapper.Map(updateStatsActionDto, statsAction);
        await _context.SaveChangesAsync();

        return statsAction;
    }


    public async Task<bool> DeleteStatsAction(string actionType)
    {
        var statsAction = await GetStatsAction(actionType);

        if (statsAction == null)
        {
            throw new Exception($"Stats action type {actionType} not found. Failed to update stats action");
        }

        _context.StatsAction.Remove(statsAction);
        await _context.SaveChangesAsync();
        return true;
    }

}