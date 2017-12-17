using Microsoft.Kinect;
using Microsoft.Kinect.VisualGestureBuilder;
using System;
using System.Collections.Generic;

namespace GestureCatcher {
    public class GestureDetector : IDisposable {
        /// <summary> 
        /// Path to the gesture database that was trained with VGB 
        /// 通过VGB训练的手势数据库的路径
        /// </summary>
        private readonly string gestureDatabasePath = @".\Stone_Right.gba";

        /// <summary> 
        /// Name of the discrete gesture in the database that we want to track 
        /// 我们要跟踪的数据库中离散手势的名称
        /// </summary>
        private readonly string seatedGestureName = "Stone_Right";

        /// <summary> 
        /// Gesture frame source which should be tied to a body tracking ID 
        /// 应绑在身体跟踪ID上的手势框架源
        /// </summary>
        private VisualGestureBuilderFrameSource vgbFrameSource = null;

        /// <summary> 
        /// Gesture frame reader which will handle gesture events coming from the sensor 
        /// 手势框架阅读器将处理来自传感器的手势事件
        /// </summary>
        private VisualGestureBuilderFrameReader vgbFrameReader = null;

        /// <summary>
        /// Initializes a new instance of the GestureDetector class along with the gesture frame source and reader
        /// 与手势框架源和阅读器一起初始化GestureDetector类的新实例
        /// </summary>
        /// <param name="kinectSensor">
        /// Active sensor to initialize the VisualGestureBuilderFrameSource object with
        /// 启动的传感器，用来初始化VisualGestureBuilderFrameSource对象
        /// </param>
        /// <param name="gestureResultView">GestureResultView object to store gesture results of a single body to</param>
        public GestureDetector(KinectSensor kinectSensor, GestureResultView gestureResultView) {
            if (kinectSensor == null) {
                throw new ArgumentNullException("kinectSensor");
            }

            if (gestureResultView == null) {
                throw new ArgumentNullException("gestureResultView");
            }

            this.GestureResultView = gestureResultView;

            // 创建vgb源文件，当一个有效的身体帧从传感器到达时，相关的身体跟踪ID将被设置。
            // create the vgb source. The associated body tracking ID will be set when a valid body frame arrives from the sensor.
            this.vgbFrameSource = new VisualGestureBuilderFrameSource(kinectSensor, 0);
            this.vgbFrameSource.TrackingIdLost += this.Source_TrackingIdLost;

            // open the reader for the vgb frames
            this.vgbFrameReader = this.vgbFrameSource.OpenReader();
            if (this.vgbFrameReader != null) {
                this.vgbFrameReader.IsPaused = true;
                this.vgbFrameReader.FrameArrived += this.Reader_GestureFrameArrived;
            }

            // 从手势数据库加载手势
            // load a gesture from the gesture database
            using (VisualGestureBuilderDatabase database = new VisualGestureBuilderDatabase(this.gestureDatabasePath)) {
                // 添加可用手势
                // we could load all available gestures in the database with a call to vgbFrameSource.AddGestures(database.AvailableGestures), 
                // but for this program, we only want to track one discrete gesture from the database, so we'll load it by name
                vgbFrameSource.AddGestures(database.AvailableGestures);
            }
        }


        /// <summary> 
        /// Gets the GestureResultView object which stores the detector results for display in the UI 
        /// GestureResultView对象用于显示结果
        /// </summary>
        public GestureResultView GestureResultView { get; private set; }

        /// <summary>
        /// Gets or sets the body tracking ID associated with the current detector
        /// 获取当前身体的ID
        /// The tracking ID can change whenever a body comes in/out of scope
        /// 跟踪ID可以随着身体进出范围而改变
        /// </summary>
        public ulong TrackingId {
            get {
                return vgbFrameSource.TrackingId;
            }

            set {
                if (vgbFrameSource.TrackingId != value) {
                    vgbFrameSource.TrackingId = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not the detector is currently paused
        /// 获取或设置一个值，指示探测器当前是否暂停
        /// If the body tracking ID associated with the detector is not valid, then the detector should be paused
        /// 如果与检测器相关的身体跟踪ID无效，则应暂停检测器
        /// </summary>
        public bool IsPaused {
            get {
                return vgbFrameReader.IsPaused;
            }

            set {
                if (vgbFrameReader.IsPaused != value) {
                    vgbFrameReader.IsPaused = value;
                }
            }
        }

        /// <summary>
        /// Disposes all unmanaged resources for the class
        /// 为该类处理所有非托管资源
        /// </summary>
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the VisualGestureBuilderFrameSource and VisualGestureBuilderFrameReader objects
        /// </summary>
        /// <param name="disposing">True if Dispose was called directly, false if the GC handles the disposing</param>
        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                if (this.vgbFrameReader != null) {
                    this.vgbFrameReader.FrameArrived -= this.Reader_GestureFrameArrived;
                    this.vgbFrameReader.Dispose();
                    this.vgbFrameReader = null;
                }

                if (this.vgbFrameSource != null) {
                    this.vgbFrameSource.TrackingIdLost -= this.Source_TrackingIdLost;
                    this.vgbFrameSource.Dispose();
                    this.vgbFrameSource = null;
                }
            }
        }

        /// <summary>
        /// Handles gesture detection results arriving from the sensor for the associated body tracking Id
        /// 处理来自传感器的相关身体追踪ID的手势检测结果
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Reader_GestureFrameArrived(object sender, VisualGestureBuilderFrameArrivedEventArgs e) {
            VisualGestureBuilderFrameReference frameReference = e.FrameReference;
            using (VisualGestureBuilderFrame frame = frameReference.AcquireFrame()) {
                if (frame != null) {
                    // get the discrete gesture results which arrived with the latest frame
                    IReadOnlyDictionary<Gesture, DiscreteGestureResult> discreteResults = frame.DiscreteGestureResults;

                    if (discreteResults != null) {
                        // we only have one gesture in this source object, but you can get multiple gestures
                        foreach (Gesture gesture in this.vgbFrameSource.Gestures) {
                            if (gesture.Name.Equals(this.seatedGestureName) && gesture.GestureType == GestureType.Discrete) {
                                discreteResults.TryGetValue(gesture, out DiscreteGestureResult result);

                                if (result != null) {
                                    // update the GestureResultView object with new gesture result values
                                    // 用新的手势结果值更新GestureResultView对象
                                    this.GestureResultView.UpdateGestureResult(true, result.Detected, result.Confidence);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles the TrackingIdLost event for the VisualGestureBuilderSource object
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Source_TrackingIdLost(object sender, TrackingIdLostEventArgs e) {
            // update the GestureResultView object to show the 'Not Tracked' image in the UI
            this.GestureResultView.UpdateGestureResult(false, false, 0.0f);
        }
    }
}
