using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAPI.Domain.Models;

namespace WebAPI.DataAccessLayer.Repositories.LeagueRepository ;

public class LeagueRepository : ILeagueRepository
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
    
    public async Task<League?> Delete(string leagueId)
    {
        try
        {
            var entity = await _db.Leagues.FindAsync(leagueId);
            if (entity == null)
            {
                _logger.LogError("League with id {leagueId} not found", leagueId);
                return null;
            }
            _db.Leagues.Remove(entity);
            await _db.SaveChangesAsync();
            
            return entity;
        }
        catch (Exception)
        {
            _logger.LogError("Error while deleting a player");
            throw;
        }
    }
    
    public async Task<League?> Update(League entity)
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
    
    public async Task<List<League>> GetAll(string? contextSearch = null)
    {
        try
        {
            IQueryable<League> query = _db.Leagues;

            if (!string.IsNullOrEmpty(contextSearch))
            {
                query = query.Where(league => league.Name.ToLower().Contains(contextSearch.ToLower()));
            }

            query = query.Include(league => league.Teams);

            return await query.ToListAsync();
        }
        catch (Exception)
        {
            _logger.LogError("Error while getting list of leagues");
            throw;
        }
    }
    
    public async Task<League?> GetById(string leagueId)
    {
        try
        {
            return await _db.Leagues
                .Include(league => league.Teams)
                .FirstOrDefaultAsync(league => league.Id == leagueId);
        }
        catch (Exception)
        {
            _logger.LogError("Error while getting list of players");
            throw;
        }
    }
}