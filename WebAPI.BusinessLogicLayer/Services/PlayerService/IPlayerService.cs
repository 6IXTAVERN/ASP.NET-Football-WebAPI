using WebAPI.Domain.Models;
using WebAPI.Domain.Response;

namespace WebAPI.BusinessLogicLayer.Services.PlayerService ;

public interface IPlayerService
{
    Task<IBaseResponse<List<Player>>> GetPlayers();
    Task<IBaseResponse<Player>> Create(Player player);
    Task<IBaseResponse<bool>> Delete(string playerId);
    Task<IBaseResponse<Player>> Update(string playerId, Player player);
    Task<IBaseResponse<Player?>> GetPlayerById(string playerId);
}