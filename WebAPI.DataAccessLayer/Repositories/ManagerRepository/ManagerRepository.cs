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
                _logger.LogError("Error while creating a player");
                throw;
            }
    }

    public async Task Delete(Manager entity)
    {
        try
        {
            _db.Managers.Remove(entity);
            await _db.SaveChangesAsync();
        }
        catch (Exception)
        {
            _logger.LogError("Error while deleting a player");
            throw;
        }
    }

    public async Task<Manager> Update(Manager entity)
    {
        try
        {
            _db.Managers.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
        catch (Exception)
        {
            _logger.LogError("Error while updating a player");
            throw;
        }
    }

    public IQueryable<Manager> GetAll()
    {
        try
        {
            return _db.Managers.AsQueryable();
        }
        catch (Exception)
        {
            _logger.LogError("Error while getting list of players");
            throw;
        }
    }

    public async Task<Manager?> GetById(string managerId)
    {
        try
        {
            return await _db.Managers.FirstOrDefaultAsync(manager => manager.Id == managerId);
        }
        catch (Exception)
        {
            _logger.LogError("Error while getting list of players");
            throw;
        }
    }
}
