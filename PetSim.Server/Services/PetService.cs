using AutoMapper;
using PetSim.Server.Data;
using PetSim.Server.Models;
namespace PetSim.Server.Services;

public class PetService {

    private readonly PetSimContext _context;
    private readonly IMapper _mapper;

    public PetService(PetSimContext context, IMapper mapper ) {
        _context = context;
        _mapper = mapper;
    }


    public async Task<Guid> CreatePet(CreatePetDto CreatePetDto)
    {
        Pet newPet = new Pet(CreatePetDto);

        _context.Pet.Add(newPet);
        await _context.SaveChangesAsync();
        // save twice because pet id exists not before 1st save
        newPet.Stats.PetId = newPet.Id;

        await _context.SaveChangesAsync();

        return newPet.Id;
    }

    public async Task<Pet> GetPet(Guid id)
    {

        var pet = await _context.Pet.FindAsync(id);

        return pet;
    }

    public async Task<Pet> UpdatePet(Guid id, UpdatePetDto petUpdate)
    {
        var pet = await GetPet(id);

        // If the pet doesn't exist, return NotFound
        if (pet != null)
        {
           _mapper.Map(petUpdate, pet);
           await _context.SaveChangesAsync();
        }

        return pet;
    }


    public async Task DeletePet(Guid id) {
        var pet = await GetPet(id);

        if (pet != null) { 
            _context.Pet.Remove(pet);
            await _context.SaveChangesAsync();
        }
     } 

    }