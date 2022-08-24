# Position, Velocity, Acceleration Converters

## Position Converters
Convert between 4 types of geographical coordinates systems.

1. **Local tangent plane** (also known as NED, ENU etc..) https://en.wikipedia.org/wiki/Local_tangent_plane_coordinates


![This is an image](/Images/ltp.png)

2. **Earth-Centered, Earth-Fixed** (also known as ECEF) https://en.wikipedia.org/wiki/Earth-centered,_Earth-fixed_coordinate_system

![This is an image](/Images/ecef.png)

3. **Lla Coordinate System** (latitude, longitude, altitude)  https://en.wikipedia.org/wiki/Geographic_coordinate_system

![This is an image](/Images/geopos.png)

4. **Local Horizontal Coordinates** (also known as polar position, or Azimuth, Elevation, Range) 

![This is an image](/Images/lhp.png)


### Usage

**Convert Lla-Position to ECEF**
```
var pc = new PositionConverter();
var lat = 32.1882286;
var lon = 34.8963593;
var alt = 500.00;
LlaPosition geo = new LlaPosition(lat, lon, alt);
EcefPosition ecef = pc.LlaToEcef(Lla);
Console.WriteLine(ecef);

// Prints
// X: 4431798.222675216, Y: 3091246.629098461, Z: 3378380.366578883
```

**Convert two Lla-position points to Local tangent plane**

```
var pc = new PositionConverter();
LlaPosition origin = new LlaPosition(-51.736538, -59.430458, 0);
LlaPosition target = new LlaPosition(-51.687572, -60.158750, 3000);
LtpPosition geoToLtp = pc.LlaToLtp(target, origin);
Console.WriteLine(geoToLtp.ToStringNed());
Console.WriteLine(geoToLtp.ToStringEnu());

// Prints
// North: 5199.166000297293, East: -50387.38398064566, Down: -2799.350897683762
// East: -50387.38398064566, North: 5199.166000297293, Up: 2799.350897683762
```

**Convert two Lla-position points to Azimuth elevation range**
```
var pc = new PositionConverter();
LlaPosition origin = new LlaPosition(4.682880, -7.965253, 0);
LlaPosition target = new LlaPosition(4.782880, -7.985253, 3000);
var azimuthElevationRange = pc.LlaToAer(origin,target );
Console.WriteLine(azimuthElevationRange);

// Prints
// Azimuth:348.6549994285944, Elevation:14.840890085414388,Distance:11673.341221811483 
```

**Convert local tangent plane to ECEF**
```
var pv = new PositionConverter();
LlaPosition origin = new LlaPosition(4.682880, -7.965253, 0);
LtpPosition ltpPosition = new LtpPosition(100.0, 200.0, 300.0);
EcefPosition ecef = pv.LtpToEcef(ltpPosition, origin);
Console.WriteLine(ecef);

// Prints
// X: 6295380.62181266, Y: -880663.1908122151, Z: 517316.47438104654
```

Image credits:

- https://standards.sedris.org
- https://ww2.mathworks.cn
- http://jhnet.co.uk
- https://www.timeanddate.com
