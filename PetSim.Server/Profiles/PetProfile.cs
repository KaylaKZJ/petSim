using AutoMapper;
using PetSim.Server.Models;

public class PetProfile : Profile
{
    public PetProfile()
    {
        CreateMap<UpdatePetDto, Pet>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);
    }
}
