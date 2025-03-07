using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchEngine.Web.Domain.Entites;

namespace SearchEngine.Web.Infrastructure.EntityConfigurations
{
    public class AirlineEntityConfiguration : IEntityTypeConfiguration<AirlineEntity>
    {
        public void Configure(EntityTypeBuilder<AirlineEntity> builder)
        {
            builder.ToTable("Airlines");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);
        }
    }
}
