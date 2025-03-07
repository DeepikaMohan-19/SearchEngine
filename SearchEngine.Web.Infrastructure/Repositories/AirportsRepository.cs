using CsvHelper;
using Microsoft.EntityFrameworkCore;
using SearchEngine.Web.Domain.Entites;
using SearchEngine.Web.Domain.Interfaces.Repositories;
using SearchEngine.Web.Infrastructure.Persistance;
using System.Globalization;

namespace SearchEngine.Web.Infrastructure.Repositories
{
    public class AirportsRepository(IDbContextFactory<FlightsDBContext> dbContextFactory) : BaseRepository<AirportEntity>(dbContextFactory), IAirportsRepository
    {
        public async Task<IEnumerable<AirportEntity>> SearchAirportsAsync(string searchTerm)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            return await dbContext.Airports
                .Where(a => !string.IsNullOrWhiteSpace(a.Name) && a.Name.Contains(searchTerm) || !string.IsNullOrWhiteSpace(a.City) && a.City.Contains(searchTerm) || !string.IsNullOrWhiteSpace(a.Country) && a.Country.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task SeedDataFromCsv(string csvFilePath)
        {
            try
            {
                using var dbContext = dbContextFactory.CreateDbContext();
                using var reader = new StreamReader(csvFilePath);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                // Read airports
                var airports = csv.GetRecords<AirportEntity>().ToList();

                var airportsToAdd = airports.Select(x => new AirportEntity
                {
                    Name = x.Name,
                    Icao = x.Icao,
                    Country = x.Country,
                    CreatedOn = DateTime.UtcNow,
                    Type = x.Type,
                    CreatedByEmail = "abc@abc.com",
                    AirportType = x.AirportType,
                    IsActive = true,
                    City = x.City,
                    Elevation = x.Elevation,
                    Iata = x.Iata,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    RunwayLength = x.RunwayLength,
                    Source = x.Source,
                    Timezone = x.Timezone

                });
                await dbContext.Airports.AddRangeAsync(airportsToAdd);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                Console.WriteLine($"Error seeding data from CSV: {ex.Message}");
            }
        }
    }
}