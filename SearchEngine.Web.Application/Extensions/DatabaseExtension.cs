using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SearchEngine.Web.Infrastructure.Persistance;

namespace SearchEngine.Web.Application.Extensions
{
    public static class DatabaseExtension
    {
        public static void InitializeDatabase(this WebApplication app)
        {
            using var serviceScope = app.Services.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<FlightsDBContext>();
            context.Database.EnsureCreated();
        }
    }
}
