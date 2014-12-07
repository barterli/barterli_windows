using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;

namespace Barter.li.Win.BL.LocationService
{

    public class LocationInfo
    {
        public double Latitude
        {
            get;
            internal set;
        }

        public double Longitude
        {
            get;
            internal set;
        }
    }

    public interface ILocationChanged
    {
        void LocationChanged(LocationInfo locationInfo);
    }

    public class GeoLocationProvider : IDisposable
    {
        private static GeoLocationProvider _Instance;
        private Geolocator geoLocater;
        private const uint locationUpdateInterval = 30 * 60 * 1000;
        private const uint locationAccuracyinMeters = 100;
        private ILocationChanged locationChangedListener;
        private LocationInfo locationInfo;
        bool _disposed = false;

        private GeoLocationProvider()
        {
            Init();
        }

        ~GeoLocationProvider()
        {
            if (!_disposed)
            {
                Dispose(false);
            }

            System.Diagnostics.Debug.WriteLine("GeoLocationProvider Finalizer called >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
        }

        private void Init()
        {
            try
            {
                geoLocater = new Geolocator();
                geoLocater.ReportInterval = locationUpdateInterval;
                locationInfo = new LocationInfo();
                geoLocater.DesiredAccuracy = PositionAccuracy.Default;
                geoLocater.DesiredAccuracyInMeters = locationAccuracyinMeters;
            }
            catch(Exception e)
            {
                throw e;
            }

        }

        private void geoLocater_PositionChanged(Geolocator sender, PositionChangedEventArgs e)
        {
            if (this.locationChangedListener != null)
            {
                locationInfo.Latitude = e.Position.Coordinate.Latitude;
                locationInfo.Longitude = e.Position.Coordinate.Longitude;

                locationChangedListener.LocationChanged(locationInfo);

            }
        }

        public static GeoLocationProvider Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new GeoLocationProvider();
                }
                return _Instance;
            }
        }


        public async Task<LocationInfo> GetGeoLocation(ILocationChanged locationChangedListener = null)
        {
            LocationInfo locationInfo = new LocationInfo();
            if (locationChangedListener != null)
            {
                this.locationChangedListener = locationChangedListener;
                geoLocater.PositionChanged += geoLocater_PositionChanged;
            }

            Geoposition tempPosition = await geoLocater.GetGeopositionAsync(new TimeSpan(0, 0, 60), new TimeSpan(0, 0, 5));
            locationInfo.Latitude = tempPosition.Coordinate.Latitude;
            locationInfo.Longitude = tempPosition.Coordinate.Longitude;
            return locationInfo;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        private void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                geoLocater.PositionChanged -= geoLocater_PositionChanged;
                geoLocater = null;
                locationInfo = null;
                locationChangedListener = null;
                _Instance = null;
                _disposed = true;

            }

            else
            {               
                geoLocater.PositionChanged -= geoLocater_PositionChanged;
                locationChangedListener = null;
            }                                   
        }
    }
}
