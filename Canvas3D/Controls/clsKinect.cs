using System;
using Microsoft.Kinect;


namespace Canvas3D.Controls {
    public class clsKinect {
        private KinectSensor kSensor = null;
        private BodyFrameReader bfReader = null;
        private static clsKinect kinect = null;
        public delegate void frameArrived(Object sender,
            BodyFrameArrivedEventArgs e);
        public event frameArrived FrameArrivedHandler;

        public clsKinect() {
            kSensor = KinectSensor.GetDefault();
            bfReader = kSensor.BodyFrameSource.OpenReader();
            bfReader.FrameArrived += Frame_Arrived;
        }

        public static clsKinect Device {
            get {
                if (kinect == null)
                    kinect = new clsKinect();
                return kinect;
            }
        }

        public void Start() {
            kSensor.Open();
        }

        public void Close() {
            bfReader?.Dispose();
            try {
                kSensor?.Close();
            } catch { }
            if (kinect != null) {
                kinect = null;
            }
        }

        private void Frame_Arrived(object sender, BodyFrameArrivedEventArgs e) {
            FrameArrivedHandler(sender, e);
        }
    }
}
