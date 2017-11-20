using System;

namespace WebApp.DAL
{
    public class TrackingInfo
    {
        public Guid ID { get; set; }

        public Guid BatchID { get; set; }

        public string BeaconID { get; set; }

        public string GpsCoordinates { get; set; }

        public string GeoReversedAddress { get; set; }

        public DateTime FrameStartTime { get; set; }

        public DateTime FrameEndTime { get; set; }

        public int ProximityAtFrameStart { get; set; } 

        public int ProximityAtFrameEnd { get; set; }

        public int MaxProximityInFrame { get; set; }

        public DateTime MaxProximityTime { get; set; }

        public int MinProximityInFrame { get; set; }

        public int MinProxmityTime { get; set; }
    }
}
