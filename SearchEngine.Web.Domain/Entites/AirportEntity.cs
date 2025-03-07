namespace SearchEngine.Web.Domain.Entites
{
    public class AirportEntity : BaseEntity
    {
        public string? City { get; set; }

        public string? Country { get; set; }

        public string? Iata { get; set; }

        public string? Icao { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public int? Elevation { get; set; }

        public string? RunwayLength { get; set; }

        public string? Type { get; set; }

        public string? Timezone { get; set; }

        public string? AirportType { get; set; }

        public string? Source { get; set; }
    }
}