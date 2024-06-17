using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAPI.Domain.Models;

namespace WebAPI.DataAccessLayer.Repositories.TeamRepository ;

public class TeamRepository : ITeamRepository
{
    private readonly DataContext _db;
    private readonly ILogger<TeamRepository> _logger;

    public TeamRepository(DataContext db, ILogger<TeamRepository> logger)
    {
        _db = db;
        _logger = logger;
    }
    
    public async Task Create(Team entity)
    {
        try
        {
            await _db.Teams.AddAsync(entity);
            await _db.SaveChangesAsync();
        }
        catch (Exception)
        {
            _logger.LogError("Error while creating a team");
            throw;
        }
    }
    
    public async Task<Team?> Delete(string teamId)
    {
        try
        {
            var entity = await _db.Teams.FindAsync(teamId);
            if (entity == null)
            {
                _logger.LogError("Team with id {teamId} not found", teamId);
                return null;
            }
            _db.Teams.Remove(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
        catch (Exception)
        {
            _logger.LogError("Error while deleting a team");
            throw;
        }
    }
    
    public async Task<Team?> Update(Team entity)
    {
        try
        {
            _db.Teams.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
        catch (Exception)
        {
            _logger.LogError("Error while updating a team");
            throw;
        }
    }
    
    public async Task<List<Team>> GetAll(string? contextSearch = null)
    {
        try
        {
            IQueryable<Team> query = _db.Teams;

            if (!string.IsNullOrEmpty(contextSearch))
            {
                query = query.Where(team => team.Name.ToLower().Contains(contextSearch.ToLower()));
            }

            query = query
                .Include(team => team.Players)
                .Include(team => team.League)
                .Include(team => team.Manager);

            return await query.ToListAsync();
        }
        catch (Exception)
        {
            _logger.LogError("Error while getting list of teams");
            throw;
        }
    }
    
    public async Task<Team?> GetById(string teamId)
    {
        try
        {
            return await _db.Teams
                .Include(team => team.Players)
                .Include(team => team.League)
                .Include(team => team.Manager)
                .FirstOrDefaultAsync(team => team.Id == teamId);
        }
        catch (Exception)
        {
            _logger.LogError("Error while getting list of teams");
            throw;
        }
    }
}