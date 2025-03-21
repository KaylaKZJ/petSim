using AutoMapper;
using PetSim.Server.Data;
using PetSim.Server.Models;
namespace PetSim.Server.Repositories;

public class PetRepository {

    private readonly PetSimContext _context;
    private readonly IMapper _mapper;

    public PetRepository(PetSimContext context, IMapper mapper ) {
        _context = context;
        _mapper = mapper;
    }


    public async Task<Guid> CreatePet(CreatePetDto createPetDto)
    {
        Pet newPet = new Pet(createPetDto);

        _context.Pet.Add(newPet);
        await _context.SaveChangesAsync();

        // save twice because pet id exists not before 1st save
        if (newPet.Stats != null) {
            newPet.Stats.PetId = newPet.Id;
        }

        await _context.SaveChangesAsync();

        return newPet.Id;
    }

    public async Task<Pet?> GetPet(Guid id)
    {
        return await _context.Pet.FindAsync(id);
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


    public async Task<bool> DeletePet(Guid id) {
        var pet = await GetPet(id);

        if (pet != null) { 
            _context.Pet.Remove(pet);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
     } 

    }