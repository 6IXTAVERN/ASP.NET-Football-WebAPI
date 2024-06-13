using WebAPI.DataAccessLayer.Repositories.PlayerRepository;

namespace WebAPI.BusinessLogicLayer.Services.PlayerService;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;
    
    public PlayerService(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }
}