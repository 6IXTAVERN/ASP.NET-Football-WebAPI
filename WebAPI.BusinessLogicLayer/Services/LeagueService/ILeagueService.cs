using WebAPI.Domain.Models;
using WebAPI.Domain.Response;

namespace WebAPI.BusinessLogicLayer.Services.LeagueService ;

public interface ILeagueService
{
    Task<IBaseResponse<League>> GetLeagueById(string leagueId);
    Task<IBaseResponse<List<League>>> GetLeagues();
    Task<IBaseResponse<League>> CreateLeague(League league);
    Task<IBaseResponse<League>> DeleteLeague(string leagueId);
    Task<IBaseResponse<League>> UpdateLeague(League league);
}