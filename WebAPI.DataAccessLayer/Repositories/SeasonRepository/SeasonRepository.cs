using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAPI.Domain.Models;

namespace WebAPI.DataAccessLayer.Repositories.SeasonRepository ;

public class SeasonRepository : ISeasonRepository
{
    private readonly DataContext _db;
    private readonly ILogger<SeasonRepository> _logger;

    public SeasonRepository(DataContext db, ILogger<SeasonRepository> logger)
    {
        _db = db;
        _logger = logger;
    }
    
    public async Task Create(Season entity)
    {
        try
        {
            await _db.Seasons.AddAsync(entity);
            await _db.SaveChangesAsync();
        }
        catch (Exception)
        {
            _logger.LogError("Error while creating a player");
            throw;
        }
    }
    
    public async Task Delete(Season entity)
    {
        try
        {
            _db.Seasons.Remove(entity);
            await _db.SaveChangesAsync();
        }
        catch (Exception)
        {
            _logger.LogError("Error while deleting a player");
            throw;
        }
    }
    
    public async Task<Season> Update(Season entity)
    {
        try
        {
            _db.Seasons.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
        catch (Exception)
        {
            _logger.LogError("Error while updating a player");
            throw;
        }
    }
    
    public IQueryable<Season> GetAll()
    {
        try
        {
            return _db.Seasons.AsQueryable();
        }
        catch (Exception)
        {
            _logger.LogError("Error while getting list of players");
            throw;
        }
    }
    
    public async Task<Season?> GetById(string seasonId)
    {
        try
        {
            return await _db.Seasons.FirstOrDefaultAsync(season => season.Id == seasonId);
        }
        catch (Exception)
        {
            _logger.LogError("Error while getting list of players");
            throw;
        }
    }
}