using System.Text.Json.Serialization;

namespace FlightPlannerNet5
{
    public class Airport
    {
          public string Country { get; set; }
          public string City { get; set; }

          [JsonPropertyName("airport")]
          public string AirportName { get; set; }
       
          public bool Equals(Airport airport)
          {
              if(airport == null)
              {
                  return false;
              }

              bool CountryCheck = this.Country == airport.Country;
              var CityCheck = this.City == airport.City;
              var AirportCodeCheck = this.AirportName == airport.AirportName;
              return CountryCheck && CityCheck && AirportCodeCheck;
          }
    }
}
