using WebAPI.DataAccessLayer.Repositories.PlayerRepository;
using WebAPI.Domain.Models;
using WebAPI.Domain.Response;

namespace WebAPI.BusinessLogicLayer.Services.PlayerService;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;
    
    public PlayerService(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }
    
    public async Task<IBaseResponse<Player>> GetPlayerById(string playerId)
    {
        try
        {
            var resume = await _playerRepository.GetById(playerId);
            // TODO: проверить на null
            return new BaseResponse<Player>("Игрок найден", StatusCode.Ok, resume);
        }
        catch (Exception ex)
        {
            return new BaseResponse<Player>(
                $"[GetResumeByUserId] : {ex.Message}",
                StatusCode.InternalServerError);
        }
    }
    
    public async Task<IBaseResponse<List<Player>>> GetPlayers()
    {
        try
        {
            var resumes = await _playerRepository.GetAll();
            
            return resumes.Count > 0 ?
                new BaseResponse<List<Player>>("Получены существующие резюме", 
                    StatusCode.Ok, resumes.ToList()) :
                new BaseResponse<List<Player>>("Найдено 0 элементов", 
                    StatusCode.Ok, resumes.ToList());
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Player>>($"[Resume.GetResumes] : {ex.Message}", 
                StatusCode.InternalServerError);
        }
    }
    
    public async Task<IBaseResponse<bool>> CreatePlayer(Player player)
    {
        try
        {
            await _playerRepository.Create(player);
            return new BaseResponse<bool>("Игрок создан", StatusCode.Ok, true);
        }
        catch (Exception ex)
        {
            return new BaseResponse<bool>(
                $"[CreatePlayer] : {ex.Message}",
                StatusCode.InternalServerError);
        }
    }
    
    public async Task<IBaseResponse<Player>> UpdatePlayer(Player player)
    {
        try
        {
            await _playerRepository.Update(player);
            return new BaseResponse<Player>("Игрок изменен", StatusCode.Ok, player);
        }
        catch (Exception ex)
        {
            return new BaseResponse<Player>(
                $"[UpdatePlayer] : {ex.Message}",
                StatusCode.InternalServerError);
        }
    }
    
    public async Task<IBaseResponse<Player>> DeletePlayer(string playerId)
    {
        try
        {
            await _playerRepository.Delete(playerId);
            return new BaseResponse<Player>("Игрок удален", StatusCode.Ok);
        }
        catch (Exception ex)
        {
            return new BaseResponse<Player>(
                $"[DeletePlayer] : {ex.Message}",
                StatusCode.InternalServerError);
        }
    }
}