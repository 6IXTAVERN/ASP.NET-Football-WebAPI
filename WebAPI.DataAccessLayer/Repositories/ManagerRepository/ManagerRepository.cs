using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAPI.Domain.Models;

namespace WebAPI.DataAccessLayer.Repositories.ManagerRepository ;

public class ManagerRepository : IManagerRepository
{
    private readonly DataContext _db;
    private readonly ILogger<ManagerRepository> _logger;

    public ManagerRepository(DataContext db, ILogger<ManagerRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task Create(Manager entity)
    {
        try
        {
            await _db.Managers.AddAsync(entity);
            await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                _logger.LogError("Error while creating a manager");
                throw;
            }
    }

    public async Task<Manager?> Delete(string managerId)
    {
        try
        {
            var entity = await _db.Managers.FindAsync(managerId);
            if (entity == null)
            {
                _logger.LogError("Manager with id {managerId} not found", managerId);
                return null;
            }
            
            _db.Managers.Remove(entity);
            await _db.SaveChangesAsync();
            
            return entity;
        }
        catch (Exception)
        {
            _logger.LogError("Error while deleting a manager");
            throw;
        }
    }

    public async Task<Manager?> Update(Manager entity)
    {
        try
        {
            _db.Managers.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
        catch (Exception)
        {
            _logger.LogError("Error while updating a manager");
            throw;
        }
    }

    public async Task<List<Manager>> GetAll(string? contextSearch = null)
    {
        try
        {
            IQueryable<Manager> query = _db.Managers;

            if (!string.IsNullOrEmpty(contextSearch))
            {
                query = query.Where(manager => manager.FullName.ToLower().Contains(contextSearch.ToLower()));
            }

            query = query.Include(manager => manager.Team);

            return await query.ToListAsync();
        }
        catch (Exception)
        {
            _logger.LogError("Error while getting list of managers");
            throw;
        }
    }

    public async Task<Manager?> GetById(string managerId)
    {
        try
        {
            return await _db.Managers
                .Include(manager => manager.Team)
                .FirstOrDefaultAsync(manager => manager.Id == managerId);
        }
        catch (Exception)
        {
            _logger.LogError("Error while getting list of managers");
            throw;
        }
    }
}
