using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetSim.Server.Data;
using PetSim.Server.Models;
namespace PetSim.Server.Services;

public class StatsService {

    private readonly PetSimContext _context;
    private readonly IMapper _mapper;

    public StatsService(PetSimContext context, IMapper mapper ) {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Stats>> GetAllStats()
    {
        return await _context.Stats.ToListAsync();
    }

    public async Task<Stats> GetStatsByPetId(Guid petId)
    {
        return  await _context.Stats.FirstOrDefaultAsync(stats => stats.PetId == petId);
    }

    public async Task<Stats> UpdateStatsByPetId(Guid petId, UpdateStatsDto statsUpdate)
    {
        var stats = await GetStatsByPetId(petId);

        // If the pet doesn't exist, return NotFound
        if (stats != null)
        {
           _mapper.Map(statsUpdate, stats);
           await _context.SaveChangesAsync();
        }

        return stats;
    }


    }