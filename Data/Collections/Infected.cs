using System;
using MongoDB.Driver.GeoJsonObjectModel;

namespace APIDio.Data.Collections
{
  public class Infected
  {
    public Infected(DateTime birth, string gender, double latitude, double longitude)
    {
      Birth = birth;
      Gender = gender;
      Localization = new GeoJson2DGeographicCoordinates(longitude, latitude);
    }

    public DateTime Birth { get; set; }

    public string Gender { get; set; }

    public GeoJson2DGeographicCoordinates Localization { get; set; }
  }
}