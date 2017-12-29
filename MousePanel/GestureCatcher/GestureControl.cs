using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace MousePanel.GestureCatcher {
    public class GestureControl {
        /// <summary> Active Kinect sensor </summary>
        private KinectSensor kinectSensor = null;

        /// <summary> Array for the bodies (Kinect will track up to 6 people simultaneously) </summary>
        private Body[] bodies = null;

        /// <summary> Reader for body frames </summary>
        private BodyFrameReader bodyFrameReader = null;

        /// <summary> List of gesture detectors, there will be one detector created for each potential body (max of 6) </summary>
        private List<GestureDetector> gestureDetectorList = null;

        Joint thumbRight, handRight, handTipRight, wristRight;

        /// <summary> 右手数据存档，以备测试 </summary>
        StreamWriter streamWriter;

        Commons.Filter.KalmanFilter kalmanFilter;

        private string dataInfo;

        public GestureControl() {
            // only one sensor is currently supported
            this.kinectSensor = KinectSensor.GetDefault();

            // set IsAvailableChanged event notifier
            this.kinectSensor.IsAvailableChanged += this.Sensor_IsAvailableChanged;

            // open the sensor
            this.kinectSensor.Open();

            // open the reader for the body frames
            this.bodyFrameReader = this.kinectSensor.BodyFrameSource.OpenReader();

            // set the BodyFramedArrived event notifier
            this.bodyFrameReader.FrameArrived += this.Reader_BodyFrameArrived;

            // initialize the gesture detection objects for our gestures
            this.gestureDetectorList = new List<GestureDetector>();

            for (int i = 0; i < kinectSensor.BodyFrameSource.BodyCount; ++i) {
                GestureResultView result = new GestureResultView(i, false, false, 0.0f);
                GestureDetector detector = new GestureDetector(kinectSensor, result);
                gestureDetectorList.Add(detector);
            }

            // 数据保留文件名 yyyy-MM-dd hh-mm-ss.txt
            string fileName = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + ".txt";
            streamWriter = new StreamWriter(fileName);
            kalmanFilter = new Commons.Filter.KalmanFilter(1e-6f, 4e-4f);
        }

        public List<GestureDetector> GestureDetectorList { get => gestureDetectorList; set => gestureDetectorList = value; }

        private void Reader_BodyFrameArrived(object sender, BodyFrameArrivedEventArgs e) {
            bool dataReceived = false;

            using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame()) {
                if (bodyFrame != null) {
                    if (bodies == null) {
                        bodies = new Body[bodyFrame.BodyCount];
                    }

                    bodyFrame.GetAndRefreshBodyData(bodies);
                    dataReceived = true;
                }
            }

            if (dataReceived) {
                if (bodies != null) {
                    int maxBodies = kinectSensor.BodyFrameSource.BodyCount;
                    for (int i = 0; i < maxBodies; ++i) {
                        Body body = bodies[i];
                        ulong trackingId = body.TrackingId;

                        if (trackingId != gestureDetectorList[i].TrackingId) {
                            gestureDetectorList[i].TrackingId = trackingId;
                            gestureDetectorList[i].IsPaused = trackingId == 0;
                        }

                        // 只要第一个人的右手，来作为鼠标位置
                        thumbRight = bodies[0].Joints[JointType.ThumbRight];
                        handRight = bodies[0].Joints[JointType.HandRight];
                        handTipRight = bodies[0].Joints[JointType.HandTipRight];
                        wristRight = kalmanFilter.Fetch(bodies[0].Joints[JointType.WristRight]);//经过滤波处理
                        gestureDetectorList[0].ResultView.UpdatePosition(wristRight.Position);
                        UpdateDataLog();
                    }
                }
            }
        }

        private void Sensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs e) {
            Debug.WriteLine(this.kinectSensor.IsAvailable ? "Get" : "Miss");
        }

        public Joint ThumbRight { get => thumbRight; }
        public Joint HandRight { get => handRight; }
        public Joint WristRight { get => wristRight; }
        public Joint HandTipRight { get => handTipRight; }
        /// <summary> 实时更新的数据标签 </summary>
        public string DataInfo { get => dataInfo; }

        private void UpdateDataLog() {
            dataInfo = string.Format("右手位置：({0},{1})", handRight.Position.X,
             handRight.Position.Y);
            streamWriter.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}",
                handRight.Position.X, handRight.Position.Y,
                handTipRight.Position.X, handTipRight.Position.Y, handTipRight.Position.Z,
                thumbRight.Position.X, thumbRight.Position.Y, thumbRight.Position.Z,
                wristRight.Position.X, wristRight.Position.Y);
        }
    }
}
