using WebAPI.DataAccessLayer.Repositories.TeamRepository;
using WebAPI.Domain.Models;
using WebAPI.Domain.Response;

namespace WebAPI.BusinessLogicLayer.Services.TeamService ;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;

    public TeamService(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<IBaseResponse<Team>> GetTeamById(string teamId)
    {
        try
        {
            var entity = await _teamRepository.GetById(teamId);
            // TODO: проверить на null
            return new BaseResponse<Team>("Команда найдена", StatusCode.Ok, entity);
        }
        catch (Exception ex)
        {
            return new BaseResponse<Team>(
                $"[GetTeamById] : {ex.Message}",
                StatusCode.InternalServerError);
        }
    }

    public async Task<IBaseResponse<List<Team>>> GetTeams()
    {
        try
        {
            var entities = await _teamRepository.GetAll();
            
            return new BaseResponse<List<Team>>(
                description: "Получены существующие команды",
                statusCode: StatusCode.Ok,
                data: entities.ToList());
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Team>>(
                description: $"[GetTeams] : {ex.Message}",
                statusCode: StatusCode.InternalServerError);
        }
    }

    public async Task<IBaseResponse<Team>> CreateTeam(Team team)
    {
        try
        {
            await _teamRepository.Create(team);
            return new BaseResponse<Team>("Команда создана", StatusCode.Ok, team);
        }
        catch (Exception ex)
        {
            return new BaseResponse<Team>(
                $"[CreateTeam] : {ex.Message}",
                StatusCode.InternalServerError);
        }
    }

    public async Task<IBaseResponse<Team>> UpdateTeam(Team team)
    {
        try
        {
            await _teamRepository.Update(team);
            return new BaseResponse<Team>("Команда изменена", StatusCode.Ok, team);
        }
        catch (Exception ex)
        {
            return new BaseResponse<Team>(
                $"[UpdateTeam] : {ex.Message}",
                StatusCode.InternalServerError);
        }
    }

    public async Task<IBaseResponse<Team>> DeleteTeam(string teamId)
    {
        try
        {
            await _teamRepository.Delete(teamId);
            return new BaseResponse<Team>("Команда удалена", StatusCode.Ok);
        }
        catch (Exception ex)
        {
            return new BaseResponse<Team>(
                $"[DeleteTeam] : {ex.Message}",
                StatusCode.InternalServerError);
        }
    }
}