# Position, Velocity, Acceleration Converters

## Position Converters
Convert between 4 types of geographical coordinates systems.

1. **Local tangent plane** (also known as NED, ENU etc..) https://en.wikipedia.org/wiki/Local_tangent_plane_coordinates


![This is an image](/Images/ltp.png)

2. **Earth-Centered, Earth-Fixed** (also known as ECEF) https://en.wikipedia.org/wiki/Earth-centered,_Earth-fixed_coordinate_system

![This is an image](/Images/ecef.png)

3. **Geographic Coordinate System** (latitude, longitude, altitude)  https://en.wikipedia.org/wiki/Geographic_coordinate_system

![This is an image](/Images/geopos.png)

4. **Local Horizontal Coordinates** (also known as polar position, or Azimuth, Elevation, Range) 

![This is an image](/Images/lhp.png)


### Usage

**Convert Geo-Position to ECEF**
```
var pc = new PositionConverter();
var lat = Angle.FromDegrees(32.1882286);
var lon = Angle.FromDegrees(34.8963593);
var alt = Distance.FromMeters(500.00);
GeoPosition geo = new GeoPosition(lat, lon, alt);
EcefPosition ecef = pc.GeoToEcef(geo);
Console.WriteLine(ecef);

// Prints
// X: 4431798.222675216, Y: 3091246.629098461, Z: 3378380.366578883
```

**Convert Geo-Position to Local tangent plane**

```
var pc = new PositionConverter();
GeoPosition origin = GeoPosition.FromDeg(-51.736538, -59.430458, 0);
GeoPosition target = GeoPosition.FromDeg(-51.687572, -60.158750, 3000);
LtpPosition geoToLtp = pc.GeoToLtp(target, origin);
Console.WriteLine(geoToLtp.ToStringNed());
Console.WriteLine(geoToLtp.ToStringEnu());

// Prints
// North: 5199.166000297293, East: -50387.38398064566, Down: -2799.350897683762
// East: -50387.38398064566, North: 5199.166000297293, Up: 2799.350897683762
```


Image credits:

- https://standards.sedris.org
- https://ww2.mathworks.cn
- http://jhnet.co.uk
- https://www.timeanddate.com
