using System.Text.Json.Serialization;

namespace FlightPlannerNet5
{
    public class Airport
    {
         [JsonIgnore]
          public int Id { get; set; }
          public string Country { get; set; }
          public string City { get; set; }

          [JsonPropertyName("airport")]
          public string AirportName { get; set; }
       
          
    }
}
