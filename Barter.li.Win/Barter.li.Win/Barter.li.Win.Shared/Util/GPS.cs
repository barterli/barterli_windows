using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Barter.li.Win.Util
{
    class GPS
    {
        public async Task<Geopoint> ExecuteGetCoordGPSCommand()
        {
            try
            {
                Geolocator geolocator = new Geolocator();
                geolocator.DesiredAccuracy = PositionAccuracy.Default;
                // Make the request for the current position

                Geoposition pos = await geolocator.GetGeopositionAsync(new TimeSpan(0, 0, 60), new TimeSpan(0, 0, 5));
                return pos.Coordinate.Point;
            }
            catch
            {
                return null;
            }
        }
    }
}
