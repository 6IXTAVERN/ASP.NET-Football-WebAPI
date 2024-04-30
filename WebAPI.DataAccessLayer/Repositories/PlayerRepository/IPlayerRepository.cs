using WebAPI.Domain.Models;

namespace WebAPI.DataAccessLayer.Repositories.PlayerRepository;

public interface IPlayerRepository : IBaseRepository<Player>
{
    public Task<Player?> GetById(string playerId);
}