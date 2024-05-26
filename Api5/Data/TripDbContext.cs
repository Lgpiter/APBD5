using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class TripDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<Client_Trip> Client_Trips { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Country_Trip> Country_Trips { get; set; }
    
    public TripDbContext(DbContextOptions<TripDbContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Konfiguracja klucza głównego dla Client_Trip
        var clientTrip = modelBuilder.Entity<Client_Trip>();
        clientTrip.HasKey(ct => new { ct.IdClient, ct.IdTrip });

        // Konfiguracja relacji Client_Trip -> Client
        clientTrip.HasOne(ct => ct.Client)
            .WithMany(c => c.Client_Trips)
            .HasForeignKey(ct => ct.IdClient);

        // Konfiguracja relacji Client_Trip -> Trip
        clientTrip.HasOne(ct => ct.Trip)
            .WithMany(t => t.Client_Trips)
            .HasForeignKey(ct => ct.IdTrip);

        // Konfiguracja klucza głównego dla Country_Trip
        var countryTrip = modelBuilder.Entity<Country_Trip>();
        countryTrip.HasKey(ct => new { ct.IdCountry, ct.IdTrip });

        // Konfiguracja relacji Country_Trip -> Country
        countryTrip.HasOne(ct => ct.Country)
            .WithMany(c => c.Country_Trips)
            .HasForeignKey(ct => ct.IdCountry);

        // Konfiguracja relacji Country_Trip -> Trip
        countryTrip.HasOne(ct => ct.Trip)
            .WithMany(t => t.Country_Trips)
            .HasForeignKey(ct => ct.IdTrip);
    }
}