namespace SearchEngine.Web.Domain.Entites
{
    public class AirlineEntity : BaseEntity
    {
        public string? Icao { get; set; }

        public string? Type { get; set; }

        public string? Country { get; set; }
    }
}