using AutoMapper;
using PetSim.Server.Models;

public class StatsProfile : Profile
{
    public StatsProfile()
    {
        CreateMap<UpdateStatsDto, PetStats>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);
    }
}
