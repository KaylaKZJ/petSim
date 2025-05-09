using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PetSim.Server.Data;
using PetSim.Server.Models;
namespace PetSim.Server.Repositories;

public class PetRepository
{

    private readonly PetSimContext _context;
    private readonly IMapper _mapper;

    public PetRepository(PetSimContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<Pet> CreatePet(CreatePetDto createPetDto)
    {

        var petType = await _context.PetTypes.FirstOrDefaultAsync(pt => pt.Type == createPetDto.Type);

        if (petType == null || petType.Type == null) throw new Exception($"Pet type {createPetDto.Type} not found");

        Pet newPet = new Pet(createPetDto, petType.Type);

        _context.Pets.Add(newPet);
        await _context.SaveChangesAsync();

        // save twice because pet id exists not before 1st save
        if (newPet.PetStats != null)
        {
            newPet.PetStats.PetId = newPet.Id;
        }

        await _context.SaveChangesAsync();

        return newPet;
    }

    public async Task<Pet?> GetPet(Guid id)
    {
        return await _context.Pets.FindAsync(id);
    }

    public async Task<Pet?> UpdatePet(Guid id, UpdatePetDto petUpdate)
    {
        var pet = await GetPet(id);

        if (pet != null)
        {
            _mapper.Map(petUpdate, pet);
            await _context.SaveChangesAsync();
        }

        return pet;
    }


    public async Task<bool> DeletePet(Guid id)
    {
        var pet = await GetPet(id);

        if (pet != null)
        {
            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

}