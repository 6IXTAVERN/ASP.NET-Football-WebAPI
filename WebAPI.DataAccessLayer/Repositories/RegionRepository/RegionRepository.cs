using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAPI.Domain.Models;

namespace WebAPI.DataAccessLayer.Repositories.RegionRepository ;

public class RegionRepository : IRegionRepository
{
    private readonly DataContext _db;
    private readonly ILogger<RegionRepository> _logger;

    public RegionRepository(DataContext db, ILogger<RegionRepository> logger)
    {
        _db = db;
        _logger = logger;
    }
    
    public async Task Create(Region entity)
    {
        try
        {
            await _db.Regions.AddAsync(entity);
            await _db.SaveChangesAsync();
        }
        catch (Exception)
        {
            _logger.LogError("Error while creating a player");
            throw;
        }
    }
    
    public async Task<Region?> Delete(string regionId)
    {
        try
        {
            var entity = await _db.Regions.FindAsync(regionId);
            if (entity == null)
            {
                _logger.LogError("Region with id {regionId} not found", regionId);
                return null;
            }
            
            _db.Regions.Remove(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
        catch (Exception)
        {
            _logger.LogError("Error while deleting a player");
            throw;
        }
    }
    
    public async Task<Region?> Update(Region entity)
    {
        try
        {
            _db.Regions.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
        catch (Exception)
        {
            _logger.LogError("Error while updating a player");
            throw;
        }
    }
    
    public async Task<List<Region>> GetAll()
    {
        try
        {
            return await _db.Regions.ToListAsync();
        }
        catch (Exception)
        {
            _logger.LogError("Error while getting list of players");
            throw;
        }
    }
    
    public async Task<Region?> GetById(string regionId)
    {
        try
        {
            return await _db.Regions.FirstOrDefaultAsync(region => region.Id == regionId);
        }
        catch (Exception)
        {
            _logger.LogError("Error while getting list of players");
            throw;
        }
    }
}