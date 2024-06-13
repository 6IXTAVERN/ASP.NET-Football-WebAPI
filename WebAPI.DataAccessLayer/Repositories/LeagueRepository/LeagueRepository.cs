using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAPI.Domain.Models;

namespace WebAPI.DataAccessLayer.Repositories.LeagueRepository ;

public class LeagueRepository : IBaseRepository<League>
{
    private readonly DataContext _db;
    private readonly ILogger<LeagueRepository> _logger;

    public LeagueRepository(DataContext db, ILogger<LeagueRepository> logger)
    {
        _db = db;
        _logger = logger;
    }
    
    public async Task Create(League entity)
    {
        try
        {
            await _db.Leagues.AddAsync(entity);
            await _db.SaveChangesAsync();
        }
        catch (Exception)
        {
            _logger.LogError("Error while creating a player");
            throw;
        }
    }
    
    public async Task Delete(League entity)
    {
        try
        {
            _db.Leagues.Remove(entity);
            await _db.SaveChangesAsync();
        }
        catch (Exception)
        {
            _logger.LogError("Error while deleting a player");
            throw;
        }
    }
    
    public async Task<League> Update(League entity)
    {
        try
        {
            _db.Leagues.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
        catch (Exception)
        {
            _logger.LogError("Error while updating a player");
            throw;
        }
    }
    
    public IQueryable<League> GetAll()
    {
        try
        {
            return _db.Leagues.AsQueryable();
        }
        catch (Exception)
        {
            _logger.LogError("Error while getting list of players");
            throw;
        }
    }
    
    public async Task<League?> GetById(string leagueId)
    {
        try
        {
            return await _db.Leagues.FirstOrDefaultAsync(league => league.Id == leagueId);
        }
        catch (Exception)
        {
            _logger.LogError("Error while getting list of players");
            throw;
        }
    }
}