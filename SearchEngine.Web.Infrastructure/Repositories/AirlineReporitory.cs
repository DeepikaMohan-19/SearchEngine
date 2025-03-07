using CsvHelper;
using Microsoft.EntityFrameworkCore;
using SearchEngine.Web.Domain.Entites;
using SearchEngine.Web.Domain.Interfaces.Repositories;
using SearchEngine.Web.Infrastructure.Persistance;
using System.Globalization;

namespace SearchEngine.Web.Infrastructure.Repositories
{
    public class AirlinesRepository(IDbContextFactory<FlightsDBContext> dbContextFactory) : BaseRepository<AirlineEntity>(dbContextFactory), IAirlinesRepository
    {
        public async Task<IEnumerable<AirlineEntity>> SearchAirlinesAsync(string searchTerm)
        {
            using var dbContext = await dbContextFactory.CreateDbContextAsync();
            return await dbContext.Airlines
                .Where(a =>
                    (a.Name != null && a.Name.Contains(searchTerm)) ||
                    (a.Icao != null && a.Icao.Contains(searchTerm)) ||
                    (a.Type != null && a.Type.Contains(searchTerm)) ||
                    (a.Country != null && a.Country.Contains(searchTerm))
                )
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
                var airports = csv.GetRecords<AirlineEntity>().ToList();

                var airportsToAdd = airports.Select(x => new AirlineEntity
                {
                    Name = x.Name,
                    Icao = x.Icao,
                    Country = x.Country,
                    CreatedOn = DateTime.UtcNow,
                    Type = x.Type,
                    CreatedByEmail = "abc@abc.com",
                });
                await dbContext.Airlines.AddRangeAsync(airportsToAdd);
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