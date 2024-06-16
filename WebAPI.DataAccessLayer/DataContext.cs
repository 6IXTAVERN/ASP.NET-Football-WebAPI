using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Domain.Models;

namespace WebAPI.DataAccessLayer ;

public class DataContext : IdentityDbContext<ApplicationUser>
{
    public required DbSet<Player> Players { get; set; }
    public required DbSet<Manager> Managers { get; set; }
    public required DbSet<Team> Teams { get; set; }
    public required DbSet<League> Leagues { get; set; }
    public required DbSet<Region> Regions { get; set; }
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
}