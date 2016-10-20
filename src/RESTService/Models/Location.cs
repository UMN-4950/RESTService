
namespace RESTService.Models
{
    public class Location
    {
        // for now it's a GUID generated in LocationRepository
        public string Key { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
