using PetSim.Server.Models;
using PetSim.Server.Repositories;
namespace PetSim.Server.Services;

public class PetService {

    private readonly PetRepository _repository;

    public PetService(PetRepository repository ) {
       _repository = repository;
    }


    public async Task<Guid> CreatePet(CreatePetDto createPetDto)
    {
        return await _repository.CreatePet(createPetDto);
    }

    public async Task<Pet?> GetPet(Guid id)
    {
        return await _repository.GetPet(id);
    }

    public async Task<Pet?> UpdatePet(Guid id, UpdatePetDto petUpdate)
    {
        return await _repository.UpdatePet(id, petUpdate);
    }


    public async Task<bool> DeletePet(Guid id) {
        return await _repository.DeletePet(id);
    }

    }