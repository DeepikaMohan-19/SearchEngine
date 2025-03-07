using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchEngine.Web.Domain.Entites;

namespace SearchEngine.Web.Infrastructure.EntityConfigurations
{
    public class AirportEntityTypeConfiguration : IEntityTypeConfiguration<AirportEntity>
    {
        public void Configure(EntityTypeBuilder<AirportEntity> builder)
        {
            builder.ToTable("Airports");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);
        }
    }
}
