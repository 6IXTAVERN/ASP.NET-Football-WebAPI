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
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Team>()
            .HasMany(t => t.Players)
            .WithOne(p => p.Team)
            .HasForeignKey(p => p.TeamId)
            .OnDelete(DeleteBehavior.SetNull);  // Установить TeamId у игроков в NULL при удалении команды 
        modelBuilder.Entity<Team>()
            .HasOne(t => t.Manager)
            .WithOne(m => m.Team)
            .HasForeignKey<Manager>(m => m.TeamId)
            .OnDelete(DeleteBehavior.SetNull); // Установить TeamId у тренера в NULL при удалении команды
    }
}