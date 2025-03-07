namespace SearchEngine.Web.Application.Configurations
{
    public class AzureSearchConfiguation
    {
        public const string ConfigurationKey = "AzureSearch";
        public string AirportsIndexName { get; set; } = null!;
        public string AirlinesIndexName { get; set; } = null!;
        public string AdminKey { get; set; } = null!;
        public string Endpoint { get; set; } = null!;
    }
}
