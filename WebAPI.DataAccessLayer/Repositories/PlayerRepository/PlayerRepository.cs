using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAPI.Domain.Models;

namespace WebAPI.DataAccessLayer.Repositories.PlayerRepository ;

public class PlayerRepository : IPlayerRepository
{
    private readonly DataContext _db;
    private readonly ILogger<PlayerRepository> _logger;

    public PlayerRepository(DataContext db, ILogger<PlayerRepository> logger)
    {
        _db = db;
        _logger = logger;
    }
    
    public async Task Create(Player entity)
    {
        try
        {
            await _db.Players.AddAsync(entity);
            await _db.SaveChangesAsync();
        }
        catch (Exception)
        {
            _logger.LogError("Error while creating a player");
            throw;
        }
    }
    
    public async Task Delete(Player entity)
    {
        try
        {
            _db.Players.Remove(entity);
            await _db.SaveChangesAsync();
        }
        catch (Exception)
        {
            _logger.LogError("Error while deleting a player");
            throw;
        }
    }
    
    public async Task<Player> Update(Player entity)
    {
        try
        {
            _db.Players.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
        catch (Exception)
        {
            _logger.LogError("Error while updating a player");
            throw;
        }
    }
    
    public IQueryable<Player> GetAll()
    {
        try
        {
            return _db.Players.AsQueryable();
        }
        catch (Exception)
        {
            _logger.LogError("Error while getting list of players");
            throw;
        }
    }
    
    public async Task<Player?> GetById(string playerId)
    {
        try
        {
            return await _db.Players.FirstOrDefaultAsync(player => player.Id == playerId);
        }
        catch (Exception)
        {
            _logger.LogError("Error while getting list of players");
            throw;
        }
    }
}