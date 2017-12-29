using Microsoft.Kinect;
using Microsoft.Kinect.VisualGestureBuilder;
using System;
using System.Collections.Generic;

namespace MousePanel.GestureCatcher {
    public class GestureDetector : IDisposable {
        /// <summary> 
        /// 通过VGB训练的手势数据库的路径
        /// </summary>
        private readonly string gestureDatabasePath = @".\Stone_Right.gba";

        /// <summary> 
        /// 我们要跟踪的数据库中离散手势的名称
        /// </summary>
        private readonly string seatedGestureName = "Stone_Right";

        /// <summary> 
        /// 应绑在身体跟踪ID上的手势框架源
        /// </summary>
        private VisualGestureBuilderFrameSource vgbFrameSource = null;

        /// <summary> 
        /// 手势框架阅读器将处理来自传感器的手势事件
        /// </summary>
        private VisualGestureBuilderFrameReader vgbFrameReader = null;

        /// <summary>
        /// 与手势框架源和阅读器一起初始化GestureDetector类的新实例
        /// </summary>
        /// <param name="kinectSensor">
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

            this.ResultView = gestureResultView;

            // 创建vgb源文件，当一个有效的身体帧从传感器到达时，相关的身体跟踪ID将被设置。
            this.vgbFrameSource = new VisualGestureBuilderFrameSource(kinectSensor, 0);
            this.vgbFrameSource.TrackingIdLost += this.Source_TrackingIdLost;

            // open the reader for the vgb frames
            this.vgbFrameReader = this.vgbFrameSource.OpenReader();
            if (this.vgbFrameReader != null) {
                this.vgbFrameReader.IsPaused = true;
                this.vgbFrameReader.FrameArrived += this.Reader_GestureFrameArrived;
            }

            // 从手势数据库加载手势
            using (VisualGestureBuilderDatabase database = new VisualGestureBuilderDatabase(this.gestureDatabasePath)) {
                // 添加可用手势
                // we could load all available gestures in the database with a call to vgbFrameSource.AddGestures(database.AvailableGestures), 
                // but for this program, we only want to track one discrete gesture from the database, so we'll load it by name
                vgbFrameSource.AddGestures(database.AvailableGestures);
            }
        }

        /// <summary> 
        /// GestureResultView对象用于显示结果
        /// </summary>
        public GestureResultView ResultView { get; private set; }

        /// <summary>
        /// 获取当前身体的ID
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
        /// 获取或设置一个值，指示探测器当前是否暂停
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
                                    // 用新的手势结果值更新GestureResultView对象
                                    this.ResultView.UpdateGestureResult(true, result.Detected, result.Confidence);
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
            this.ResultView.UpdateGestureResult(false, false, 0.0f);
        }
    }
}
