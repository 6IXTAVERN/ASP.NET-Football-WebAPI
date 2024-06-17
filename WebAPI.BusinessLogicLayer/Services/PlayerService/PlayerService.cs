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
            var entity = await _playerRepository.GetById(playerId);
                        
            if (entity == null)
            {
                return new BaseResponse<Player>("Игрок не найден", StatusCode.NotFound);
            }

            return new BaseResponse<Player>("Игрок найден", StatusCode.Ok, entity);
        }
        catch (Exception ex)
        {
            return new BaseResponse<Player>(
                $"[GetPlayerById] : {ex.Message}",
                StatusCode.InternalServerError);
        }
    }
    
    public async Task<IBaseResponse<List<Player>>> GetPlayers(string? contextSearch = null)
    {
        try
        {
            var entities = await _playerRepository.GetAll(contextSearch);
            
            return new BaseResponse<List<Player>>(
                description: "Получены существующие игроки",
                statusCode: StatusCode.Ok,
                data: entities);
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Player>>($"[GetPlayers] : {ex.Message}", 
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