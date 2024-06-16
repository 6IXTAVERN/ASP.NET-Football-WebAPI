using AutoMapper;
using WebAPI.Domain.DTO.Player;
using WebAPI.Domain.Models;
using WebAPI.DTO.PlayerDTO;


namespace WebAPI ;
    
public class MappingProfiles : Profile {
    public MappingProfiles() {
        CreateMap<Player, CreatePlayerDto>().ReverseMap();
        CreateMap<Player, PlayerDto>().ReverseMap();
        CreateMap<Player, UpdatePlayerDto>().ReverseMap();
        
        // CreateMap<Club, ClubDTO>().ReverseMap();
        // CreateMap<Club, CreateClubDTO>().ReverseMap();
        // CreateMap<Club, UpdateClubDTO>().ReverseMap();
        //
        // CreateMap<Country, CreateCountryDTO>().ReverseMap();
        // CreateMap<Country, CountryDTO>().ReverseMap();
        // CreateMap<Country, UpdateCountryDTO>().ReverseMap();
    }
}
    