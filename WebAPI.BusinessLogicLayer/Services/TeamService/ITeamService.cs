using WebAPI.Domain.Models;
using WebAPI.Domain.Response;

namespace WebAPI.BusinessLogicLayer.Services.TeamService ;

public interface ITeamService
{
    Task<IBaseResponse<Team>> GetTeamById(string teamId);
    Task<IBaseResponse<List<Team>>> GetTeams();
    Task<IBaseResponse<Team>> CreateTeam(Team team);
    Task<IBaseResponse<Team>> DeleteTeam(string teamId);
    Task<IBaseResponse<Team>> UpdateTeam(Team team);
}