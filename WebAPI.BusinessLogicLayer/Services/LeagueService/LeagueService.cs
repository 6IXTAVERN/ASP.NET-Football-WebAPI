using WebAPI.DataAccessLayer.Repositories.LeagueRepository;
using WebAPI.Domain.Models;
using WebAPI.Domain.Response;

namespace WebAPI.BusinessLogicLayer.Services.LeagueService ;

public class LeagueService : ILeagueService
{
    private readonly ILeagueRepository _leagueRepository;

    public LeagueService(ILeagueRepository leagueRepository)
    {
        _leagueRepository = leagueRepository;
    }

    public async Task<IBaseResponse<League>> GetLeagueById(string leagueId)
    {
        try
        {
            var entity = await _leagueRepository.GetById(leagueId);
            // TODO: проверить на null
            return new BaseResponse<League>("Лига найдена", StatusCode.Ok, entity);
        }
        catch (Exception ex)
        {
            return new BaseResponse<League>(
                $"[GetLeagueById] : {ex.Message}",
                StatusCode.InternalServerError);
        }
    }

    public async Task<IBaseResponse<List<League>>> GetLeagues(string? contextSearch = null)
    {
        try
        {
            var entities = await _leagueRepository.GetAll(contextSearch);
            
            return new BaseResponse<List<League>>(
                description: "Получены существующие лиги",
                statusCode: StatusCode.Ok,
                data: entities.ToList());
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<League>>(
                description: $"[GetLeagues] : {ex.Message}",
                statusCode: StatusCode.InternalServerError);
        }
    }

    public async Task<IBaseResponse<bool>> CreateLeague(League league)
    {
        try
        {
            // TODO: разные ситуации предусмотреть
            await _leagueRepository.Create(league);
            return new BaseResponse<bool>("Лига создана", StatusCode.Ok, true);
        }
        catch (Exception ex)
        {
            return new BaseResponse<bool>(
                $"[CreateLeague] : {ex.Message}",
                StatusCode.InternalServerError, false);
        }
    }

    public async Task<IBaseResponse<League>> UpdateLeague(League league)
    {
        try
        {
            // TODO: разные ситуации предусмотреть
            var entity = await _leagueRepository.Update(league);
            return new BaseResponse<League>("Лига изменена", StatusCode.Ok, entity);
        }
        catch (Exception ex)
        {
            return new BaseResponse<League>(
                $"[UpdateLeague] : {ex.Message}",
                StatusCode.InternalServerError);
        }
    }

    public async Task<IBaseResponse<League>> DeleteLeague(string leagueId)
    {
        try
        {
            await _leagueRepository.Delete(leagueId);
            return new BaseResponse<League>("Лига удалена", StatusCode.Ok);
        }
        catch (Exception ex)
        {
            return new BaseResponse<League>(
                $"[DeleteLeague] : {ex.Message}",
                StatusCode.InternalServerError);
        }
    }
}
