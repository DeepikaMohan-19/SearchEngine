namespace SearchEngine.Web.Application.DTOs
{
    public class AirlineDto : BaseDto
    {
        public string? Icao { get; set; }

        public string? Type { get; set; }

        public string? Country { get; set; }
    }
}
