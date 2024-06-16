using AutoMapper;
using WebAPI.Domain.DTO.Player;
using WebAPI.Domain.Models;
using WebAPI.DTO.LeagueDTO;
using WebAPI.DTO.PlayerDTO;


namespace WebAPI ;
    
public class MappingProfiles : Profile {
    public MappingProfiles() {
        CreateMap<Player, CreatePlayerDto>().ReverseMap();
        CreateMap<Player, PlayerDto>().ReverseMap();
        CreateMap<Player, UpdatePlayerDto>().ReverseMap();
        
        CreateMap<League, LeagueDto>().ReverseMap();
        CreateMap<League, CreateLeagueDto>().ReverseMap();
        CreateMap<League, UpdateLeagueDto>().ReverseMap();
        //
        // CreateMap<Country, CreateCountryDTO>().ReverseMap();
        // CreateMap<Country, CountryDTO>().ReverseMap();
        // CreateMap<Country, UpdateCountryDTO>().ReverseMap();
    }
}
    