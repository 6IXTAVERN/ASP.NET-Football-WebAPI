using AutoMapper;
using WebAPI.Domain.Models;
using WebAPI.DTO.LeagueDTO;
using WebAPI.DTO.ManagerDTO;
using WebAPI.DTO.PlayerDTO;
using WebAPI.DTO.RegionDTO;
using WebAPI.DTO.TeamDTO;


namespace WebAPI ;
    
public class MappingProfiles : Profile {
    public MappingProfiles() {
        CreateMap<Player, CreatePlayerDto>().ReverseMap();
        CreateMap<Player, PlayerDto>().ReverseMap();
        CreateMap<Player, UpdatePlayerDto>().ReverseMap();
        
        CreateMap<Manager, ManagerDto>().ReverseMap();
        CreateMap<Manager, CreateManagerDto>().ReverseMap();
        CreateMap<Manager, UpdateManagerDto>().ReverseMap();
        
        CreateMap<Team, TeamDto>().ReverseMap();
        CreateMap<Team, CreateTeamDto>().ReverseMap();
        CreateMap<Team, UpdateTeamDto>().ReverseMap();
        
        CreateMap<League, LeagueDto>().ReverseMap();
        CreateMap<League, CreateLeagueDto>().ReverseMap();
        CreateMap<League, UpdateLeagueDto>().ReverseMap();
        
        CreateMap<Region, RegionDto>().ReverseMap();
        CreateMap<Region, CreateRegionDto>().ReverseMap();
        CreateMap<Region, UpdateRegionDto>().ReverseMap();
    }
}
    