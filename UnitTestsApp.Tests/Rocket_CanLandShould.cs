using System;
using Xunit;
using UnitTestsApp.RocketLib;

namespace UnitTestsApp.Tests;

public class Rocket_CanLandShould
{
    private readonly LandingArea _platform;

    public Rocket_CanLandShould()
    {
        var random = new Random();

        int landingAreaXDimention = random.Next();
        int landingAreaYDimention = random.Next();
        int landingPlatformXDimention = random.Next(0, landingAreaXDimention);
        int landingPlatformYDimention = random.Next(0, landingAreaYDimention);
        int landingPlatformXPozition = random.Next(0, landingAreaXDimention - landingPlatformXDimention);
        int landingPlatformYPozition = random.Next(0, landingAreaYDimention - landingPlatformYDimention);

        _platform = new LandingArea(landingAreaXDimention,
            landingAreaYDimention,
            landingPlatformXDimention,
            landingPlatformYDimention,
            landingPlatformXPozition,
            landingPlatformYPozition);
    }

    [Fact]
    public void CanLandReturnOk()
    {
        var r = new Random();

        var x = r.Next(_platform.LandingPlatformXPozition, _platform.LandingPlatformXPozition + _platform.LandingPlatformXDimention);
       
        var y = r.Next(_platform.LandingPlatformYPozition, _platform.LandingPlatformYPozition + _platform.LandingPlatformYDimention);

        Rocket rocket = new("r1");

        string result = _platform.CanLand(x, y, rocket);

        Assert.Equal("ok for landing", result);
    }

    [Fact]
    public void CanLandReturnOut()
    {
        var r = new Random();

        var inX = r.Next(_platform.LandingPlatformXPozition, _platform.LandingPlatformXPozition + _platform.LandingPlatformXDimention);
        
        var inY = r.Next(_platform.LandingPlatformYPozition, _platform.LandingPlatformYPozition + _platform.LandingPlatformYDimention);

        Rocket rocket = new("r1");

        string result1 = _platform.CanLand(inX, _platform.LandingPlatformYPozition + _platform.LandingPlatformYDimention + 1, rocket);

        Assert.Equal("out of platform", result1);

        string result2 = _platform.CanLand(_platform.LandingPlatformXPozition + _platform.LandingPlatformXDimention + 1, inY, rocket);

        Assert.Equal("out of platform", result2);

        string result3 = _platform.CanLand(_platform.LandingPlatformXPozition + _platform.LandingPlatformXDimention + 1, _platform.LandingPlatformYPozition + _platform.LandingPlatformYDimention + 1, rocket);

        Assert.Equal("out of platform", result3);
    }

    [Fact]
    public void CanLandReturnClash()
    {
        var r = new Random();

        var x = r.Next(_platform.LandingPlatformXPozition, _platform.LandingPlatformXPozition + _platform.LandingPlatformXDimention);

        var y = r.Next(_platform.LandingPlatformYPozition, _platform.LandingPlatformYPozition + _platform.LandingPlatformYDimention);

        Rocket rocket1 = new("r1");

        _ = _platform.CanLand(x, y, rocket1);

        Rocket rocket2 = new("r2");

        string result1 = _platform.CanLand(x, y, rocket2);

        Assert.Equal("clash", result1);

        string result2 = _platform.CanLand(x + 1, y, rocket2);

        Assert.Equal("clash", result2);

        string result3 = _platform.CanLand(x - 1, y, rocket2);

        Assert.Equal("clash", result3);

        string result4 = _platform.CanLand(x, y + 1, rocket2);

        Assert.Equal("clash", result4);

        string result5 = _platform.CanLand(x, y - 1, rocket2);

        Assert.Equal("clash", result5);

        string result6 = _platform.CanLand(x + 1, y + 1, rocket2);

        Assert.Equal("clash", result6);

        string result7 = _platform.CanLand(x - 1, y - 1, rocket2);

        Assert.Equal("clash", result7);

        string result8 = _platform.CanLand(x + 1, y - 1, rocket2);

        Assert.Equal("clash", result8);

        string result9 = _platform.CanLand(x - 1, y + 1, rocket2);

        Assert.Equal("clash", result9);

        string result10 = _platform.CanLand(x + 2, y + 2, rocket2);

        Assert.Equal("ok for landing", result10);
    }
}
