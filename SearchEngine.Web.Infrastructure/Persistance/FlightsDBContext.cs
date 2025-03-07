using Microsoft.EntityFrameworkCore;
using SearchEngine.Web.Domain.Entites;
using System.Reflection;

namespace SearchEngine.Web.Infrastructure.Persistance;

public class FlightsDBContext(DbContextOptions<FlightsDBContext> options) : DbContext(options)
{
    public virtual DbSet<AirlineEntity> Airlines { get; set; }

    public virtual DbSet<AirportEntity> Airports { get; set; }

    public virtual DbSet<UserEntity> Users { get; set; }

    public virtual DbSet<SearchHistoryEntity> UserSearchHistory { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}