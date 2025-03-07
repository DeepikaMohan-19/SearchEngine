namespace SearchEngine.Web.Domain.Entites
{
    public class SearchHistoryEntity : BaseEntity
    {
        public string SearchTerm { get; set; } = null!;
    }
}
