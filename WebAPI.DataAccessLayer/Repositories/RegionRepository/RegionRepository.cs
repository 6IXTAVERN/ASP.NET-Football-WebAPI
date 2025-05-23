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
            _logger.LogError("Error while creating a region");
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
            _logger.LogError("Error while deleting a region");
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
            _logger.LogError("Error while updating a region");
            throw;
        }
    }
    
    public async Task<List<Region>> GetAll(string? contextSearch = null)
    {
        try
        {
            IQueryable<Region> query = _db.Regions;

            if (!string.IsNullOrEmpty(contextSearch))
            {
                query = query.Where(region => region.Name.ToLower().Contains(contextSearch.ToLower()));
            }

            query = query.Include(region => region.Leagues);

            return await query.ToListAsync();
        }
        catch (Exception)
        {
            _logger.LogError("Error while getting list of regions");
            throw;
        }
    }
    
    public async Task<Region?> GetById(string regionId)
    {
        try
        {
            return await _db.Regions
                .Include(region => region.Leagues)
                .FirstOrDefaultAsync(region => region.Id == regionId);
        }
        catch (Exception)
        {
            _logger.LogError("Error while getting list of regions");
            throw;
        }
    }
}