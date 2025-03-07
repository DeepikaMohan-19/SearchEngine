using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SearchEngine.Web.Domain.Entites;

namespace SearchEngine.Web.Infrastructure.EntityConfigurations
{
    public class SearchHistoryEntityConfiguration : IEntityTypeConfiguration<SearchHistoryEntity>
    {
        public void Configure(EntityTypeBuilder<SearchHistoryEntity> builder)
        {
            builder.ToTable("UserSearchHistory");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);
        }
    }
}
