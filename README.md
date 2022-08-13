# 3D Position, Velocity, Acceleration Converters

A simple .NET library that is designed to assist with geographic coordinats, velocity and acceleration conversions.
There are many libraries out there, but I couldnt find one that meets all my needs, especialy 3D conversions.

### Is this code usefull for you? Let me know!
This library was built for one of my projects. Please make effort worth while by dropping me a line and telling me what you are using it for.
I would especialy appreciate:
- Addtional unit tests for functionality I have not yet covered.
- Documentation additions.


### Releases and Licensing
I am not releasing this software in an official release at the moment.

You are free to download it, and use it as you like, give credit, or not.

If you decide to release this yourself, please give credit to this project.



## Position Converters
Convert between 4 types of geographical coordinates systems.

1. **Local tangent plane** (also known as NED, ENU etc..) https://en.wikipedia.org/wiki/Local_tangent_plane_coordinates


![This is an image](/Images/ltp.png)

2. **Earth-Centered, Earth-Fixed** (also known as ECEF) https://en.wikipedia.org/wiki/Earth-centered,_Earth-fixed_coordinate_system

![This is an image](/Images/ecef.png)

3. **Geographic Coordinate System** (latitude, longitude, altitude)  https://en.wikipedia.org/wiki/Geographic_coordinate_system

![This is an image](/Images/geopos.png)

4. **Local Horizontal Coordinates** (also known as polar position, or Azimuth, Elevation, Range)  https://en.wikipedia.org/wiki/Horizontal_coordinate_system

![This is an image](/Images/lhp.png)


### Usage

**Convert Geo-Position to ECEF**
```c#
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

**Convert two geo-position points to Local tangent plane**

```c#
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

**Convert two geo-position points to Azimuth elevation range**
```c#
var pc = new PositionConverter();
GeoPosition origin = GeoPosition.FromDeg(4.682880, -7.965253, 0);
GeoPosition target = GeoPosition.FromDeg(4.782880, -7.985253, 3000);
var azimuthElevationRange = pc.GeoToAer(origin,target );
Console.WriteLine(azimuthElevationRange);

// Prints
// Azimuth:348.6549994285944, Elevation:14.840890085414388,Distance:11673.341221811483 
```

**Convert local tangent plane to ECEF**
```c#
var pv = new PositionConverter();
GeoPosition origin = GeoPosition.FromDeg(4.682880, -7.965253, 0);
LtpPosition ltpPosition = new LtpPosition(100.0, 200.0, 300.0);
EcefPosition ecef = pv.LtpToEcef(ltpPosition, origin);
Console.WriteLine(ecef);

// Prints
// X: 6295380.62181266, Y: -880663.1908122151, Z: 517316.47438104654
```

**Convert local tangent plane to Azimuth, Elevation, Distance**
```c#
var pv = new PositionConverter();
LtpPosition ltpPosition = new LtpPosition(100.0, 200.0, -300.0);
var azimuthElevationRange = pv.LtpToAer(ltpPosition);
Console.WriteLine(azimuthElevationRange);

// Prints
// Azimuth:63.434948822922, Elevation:53.300774799510116,Distance:374.16573867739413 
```

Image credits:

- https://standards.sedris.org
- https://ww2.mathworks.cn
- http://jhnet.co.uk
- https://www.timeanddate.com
