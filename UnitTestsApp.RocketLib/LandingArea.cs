
using System.Drawing;

namespace UnitTestsApp.RocketLib;

public struct LandingArea
{
    public readonly int LandingAreaXDimention;
    public readonly int LandingAreaYDimention;
    public readonly int LandingPlatformXDimention;
    public readonly int LandingPlatformYDimention;
    public readonly int LandingPlatformXPozition;
    public readonly int LandingPlatformYPozition;

    private Dictionary<string, (int x, int y)> previousRocketLandings = new();

    public LandingArea(
        int landingAreaXDimention,
        int landingAreaYDimention,
        int landingPlatformXDimention,
        int landingPlatformYDimention,
        int landingPlatformXPozition,
        int landingPlatformYPozition)
    {
        if (landingPlatformXPozition > landingAreaXDimention || landingPlatformYPozition > landingAreaYDimention)
        {
            throw new ArgumentException("Landing Platform cannot be outside the landing area");
        }
        if (landingPlatformXPozition + landingPlatformXDimention > landingAreaXDimention 
            || landingPlatformYPozition + landingPlatformYDimention > landingAreaYDimention)
        {
            throw new ArgumentException("Landing Platform cannot be outside the landing area");
        }
        LandingAreaXDimention = landingAreaXDimention;
        LandingAreaYDimention = landingAreaYDimention;
        LandingPlatformXDimention = landingPlatformXDimention;
        LandingPlatformYDimention = landingPlatformYDimention;
        LandingPlatformXPozition = landingPlatformXPozition;
        LandingPlatformYPozition = landingPlatformYPozition;

    }

    public string CanLand(int x, int y, Rocket rocket)
    {
        if (x < LandingPlatformXPozition 
            || x > LandingPlatformXPozition + LandingPlatformXDimention
            || y < LandingPlatformYPozition
            || y > LandingPlatformYPozition + LandingPlatformYDimention
            )
        {
            return "out of platform";
        }

        var point = (x, y);

        if (previousRocketLandings.Any(x => Math.Abs(x.Value.x - point.x) <= 1 || Math.Abs(x.Value.y - point.y) <= 1))
        {
            return "clash";
        }

        if (previousRocketLandings.ContainsKey(rocket.Name))
        {
            previousRocketLandings[rocket.Name] = (x, y);
        }
        else
        {
            previousRocketLandings.Add(rocket.Name, (x, y));
        }

        return "ok for landing";
    }
}
