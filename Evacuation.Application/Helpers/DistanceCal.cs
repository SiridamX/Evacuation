namespace Evacuation.Application.Helper;

public static class DistanceCalculator
{
    public static double HaversineDistance(
    double originLatitude,
    double originLongitude,
    double destinationLatitude,
    double destinationLongitude)
    {
        const double earthRadiusKm = 6371;

        double deltaLatitude = ToRadians(destinationLatitude - originLatitude);
        double deltaLongitude = ToRadians(destinationLongitude - originLongitude);

        double originLatitudeRad = ToRadians(originLatitude);
        double destinationLatitudeRad = ToRadians(destinationLatitude);

        double x1 =
            Math.Pow(Math.Sin(deltaLatitude / 2), 2) +
            Math.Cos(originLatitudeRad) *
            Math.Cos(destinationLatitudeRad) *
            Math.Pow(Math.Sin(deltaLongitude / 2), 2);

        double x2 = 2 * Math.Atan2(Math.Sqrt(x1), Math.Sqrt(1 - x1));

        return earthRadiusKm * x2;
    }

    private static double ToRadians(double degrees)
        => degrees * Math.PI / 180.0;
}