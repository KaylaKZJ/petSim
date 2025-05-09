using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetSim.Server.Data;
using PetSim.Server.Models;
namespace PetSim.Server.Repositories;

public class PetStatsRepository
{

    private readonly PetSimContext _context;
    private readonly IMapper _mapper;

    public PetStatsRepository(PetSimContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PetStats>> GetAllStats()
    {
        return await _context.PetStats.ToListAsync();
    }

    public async Task<PetStats?> GetStatsByPetId(Guid petId)
    {
        return await _context.PetStats.FirstOrDefaultAsync(stats => stats.PetId == petId);
    }

    public async Task<PetStats?> UpdateStatsByPetId(Guid petId, UpdateStatsDto statsUpdate)
    {
        var stats = await GetStatsByPetId(petId);

        if (stats != null)
        {
            _mapper.Map(statsUpdate, stats);
            await _context.SaveChangesAsync();
        }

        return stats;
    }


}