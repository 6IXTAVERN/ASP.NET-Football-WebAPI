using WebAPI.Domain.Models;
using WebAPI.Domain.Response;

namespace WebAPI.BusinessLogicLayer.Services.PlayerService ;

public interface IPlayerService
{
    Task<IBaseResponse<Player>> GetPlayerById(string playerId);
    Task<IBaseResponse<List<Player>>> GetPlayers();
    Task<IBaseResponse<Player>> CreatePlayer(Player player);
    Task<IBaseResponse<Player>> DeletePlayer(string playerId);
    Task<IBaseResponse<Player>> UpdatePlayer(Player player);
}