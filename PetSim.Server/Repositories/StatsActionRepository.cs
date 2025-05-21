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


    public async Task<Stats> TakeStatsAction(TakeStatsActionDto takeStatsActionDto)
    {
        var statsAction = await _context.StatsAction.FirstOrDefaultAsync(sa => sa.Type == takeStatsActionDto.Type);
        if (statsAction == null)
        {
            throw new Exception($"{takeStatsActionDto.Type} was not found");
        }
        var petStats = await _context.PetStats.FirstOrDefaultAsync(stats => stats.PetId == takeStatsActionDto.PetId);
        if (petStats == null)
        {
            throw new Exception($"Pet of Id {takeStatsActionDto.PetId} was not found");
        }

        var statsDistribution = await _context.StatsDistribution.FirstOrDefaultAsync(sd => sd.Id == statsAction.StatsDistributionId);
        if (statsDistribution == null)
        {
            throw new Exception($"statsDistribution of statsActionId {statsAction.Id} was not found");
        }

        petStats.Stats.Loneliness = StatsActionHelper.AddStat(petStats.Stats.Loneliness, statsDistribution.Stats.Loneliness);
        petStats.Stats.Happiness = StatsActionHelper.AddStat(petStats.Stats.Happiness, statsDistribution.Stats.Happiness);
        petStats.Stats.Hunger = StatsActionHelper.AddStat(petStats.Stats.Hunger, statsDistribution.Stats.Hunger);
        petStats.Stats.Boredom = StatsActionHelper.AddStat(petStats.Stats.Boredom, statsDistribution.Stats.Boredom);
        petStats.Stats.Weight = StatsActionHelper.AddStat(petStats.Stats.Weight, statsDistribution.Stats.Weight);

        await _context.SaveChangesAsync();

        return petStats.Stats;
    }
    public async Task<StatsAction> CreateStatsAction(CreateStatsActionDto createStatsActionDto)
    {
        var petTypes = createStatsActionDto.PetTypes.Where(pt => pt != null).Select(pt => pt.Type).ToList();

        var existingPetTypes = await _context.PetTypes
            .Where(pt => petTypes.Contains(pt.Type))
            .Select(pt => pt.Type)
            .ToListAsync();

        var missingPetTypes = petTypes.Except(existingPetTypes).ToList();

        if (missingPetTypes.Any())
        {
            throw new Exception($"Pet types not found: {string.Join(", ", missingPetTypes)}");
        }


        var statsActionId = Guid.NewGuid();

        var statsAction = new StatsAction
        {
            Id = statsActionId,
            Type = createStatsActionDto.Type,
            PetTypes = createStatsActionDto.PetTypes,
        };

        var statsDistribution = new StatsDistribution
        {
            Id = Guid.NewGuid(),
            StatsActionId = statsActionId,
            StatsAction = statsAction,
            Stats = createStatsActionDto.StatsDistribution.Stats
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