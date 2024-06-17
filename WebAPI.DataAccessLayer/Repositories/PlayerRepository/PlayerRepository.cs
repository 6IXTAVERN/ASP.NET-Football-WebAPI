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
    
    public async Task<Player?> Delete(string playerId)
    {
        try
        {
            var entity = await _db.Players.FindAsync(playerId);
            if (entity == null)
            {
                _logger.LogError("Player with id {playerId} not found", playerId);
                return null;
            }
            
            _db.Players.Remove(entity);
            await _db.SaveChangesAsync();
            
            return entity;
        }
        catch (Exception)
        {
            _logger.LogError("Error while deleting a player");
            throw;
        }
    }
    
    public async Task<Player?> Update(Player entity)
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
    
    public async Task<List<Player>> GetAll(string? contextSearch = null)
    {
        try
        {
            IQueryable<Player> query = _db.Players;

            if (!string.IsNullOrEmpty(contextSearch))
            {
                query = query.Where(player => player.FullName.ToLower().Contains(contextSearch.ToLower()));
            }

            query = query.Include(player => player.Team);

            return await query.ToListAsync();
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
            return await _db.Players
                .Include(player => player.Team)
                .FirstOrDefaultAsync(player => player.Id == playerId);
        }
        catch (Exception)
        {
            _logger.LogError("Error while getting list of players");
            throw;
        }
    }
}