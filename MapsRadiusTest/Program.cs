// See https://aka.ms/new-console-template for more information

LocationService ls = new LocationService();
List<driverLocation> users = new List<driverLocation>();
users.Add(new driverLocation { Latitude = 24.877287817795274, Longitude = 67.08791201970031 });
users.Add(new driverLocation { Latitude = 24.877670418491334, Longitude = 67.08476262208455 });
users.Add(new driverLocation { Latitude = 24.85280830993382, Longitude = 67.02370224907186 });
users.Add(new driverLocation { Latitude = 24.948155734433595, Longitude = 67.1835861699919 });

var res = ls.GetNearbyDrivers(users, 24.87958186491742, 67.08686249420916, 20);

foreach (var user in res)
{
    Console.WriteLine("Lat: {0}, Lng: {1}, Distance: {2} km, Duration: {3} ", user.Latitude, user.Longitude, user.Distance, user.Duration);
}

public class driverLocation
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

public class NearByDriver
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Distance { get; set; }  
    public double Duration { get; set; }
}

public class LocationService
{
    public List<NearByDriver> GetNearbyDrivers(List<driverLocation> users, double latitude, double longitude, double maxDistance)
    {
        const double avgSpeed = 40;

        List<NearByDriver> nearbyUsers = new List<NearByDriver>();

        foreach (var user in users)
        {
            double distance = CalculateDistance(latitude, longitude, user.Latitude, user.Longitude);
            double duration = (double)(distance / avgSpeed) * 60;

            if (distance <= maxDistance)
            {
                nearbyUsers.Add(new NearByDriver { Distance = distance, Latitude = user.Latitude, Longitude = user.Longitude, Duration = duration});
            }
        }

        return nearbyUsers;
    }

    private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        // Implementation of the Haversine formula to calculate distance between two points
        // You can replace this with a different distance calculation formula if needed

        const double EarthRadiusKm = 6371; // Radius of the Earth in kilometers

        var dLat = DegreeToRadian(lat2 - lat1);
        var dLon = DegreeToRadian(lon2 - lon1);

        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(DegreeToRadian(lat1)) * Math.Cos(DegreeToRadian(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        var distance = EarthRadiusKm * c;

        return distance;
    }


    private double DegreeToRadian(double degree)
    {
        return degree * Math.PI / 180;
    }
}