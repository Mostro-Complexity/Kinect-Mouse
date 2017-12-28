using System.Diagnostics;

namespace MousePanel.GestureCatcher
{
    public sealed class GestureResultView
    {
        public delegate void KinectClickEventHandler();
        public event KinectClickEventHandler KinectClickUpEvent, KinectClickDownEvent,
            KinectClickUpRightEvent, KinectClickDownRightEvent;
        int start_time, end_time, run_time;
        /// <summary> The body index (0-5) associated with the current gesture detector </summary>
        private int bodyIndex = 0;

        /// <summary> Current confidence value reported by the discrete gesture </summary>
        private float confidence = 0.0f;

        /// <summary> True, if the discrete gesture is currently being detected </summary>
        private bool detected = false;

        /// <summary> True, if the body is currently being tracked </summary>
        private bool isTracked = false;

        /// <summary>
        /// Initializes a new instance of the GestureResultView class and sets initial property values
        /// </summary>
        /// <param name="bodyIndex">Body Index associated with the current gesture detector</param>
        /// <param name="isTracked">True, if the body is currently tracked</param>
        /// <param name="detected">True, if the gesture is currently detected for the associated body</param>
        /// <param name="confidence">Confidence value for detection of the 'Seated' gesture</param>
        public GestureResultView(int bodyIndex, bool isTracked, bool detected, float confidence)
        {
            this.bodyIndex = bodyIndex;
            this.isTracked = isTracked;
            this.detected = detected;
            this.confidence = confidence;
            start_time = -1;
        }

        /// <summary>
        /// Updates the values associated with the discrete gesture detection result
        /// 更新与离散手势检测结果相关的值
        /// </summary>
        /// <param name="isBodyTrackingIdValid">
        /// True, if the body associated with the GestureResultView object is still being tracked
        /// 如果与GestureResultView对象相关的主体仍在被追踪为True
        /// </param>
        /// <param name="isGestureDetected">
        /// True, if the discrete gesture is currently detected for the associated body
        /// 如果当前检测到相关联的身体的离散手势为True
        /// </param>
        /// <param name="detectionConfidence">
        /// Confidence value for detection of the discrete gesture
        /// 用于检测离散手势的置信度值
        /// </param>
        public void UpdateGestureResult(bool isBodyTrackingIdValid, bool isGestureDetected, float detectionConfidence)
        {
            isTracked = isBodyTrackingIdValid;
            confidence = 0.0f;
            if (!isTracked)
            {
                detected = false;
                Debug.WriteLine("Miss");
                KinectClickUpEvent();
            }
            else
            {
                detected = isGestureDetected;
                if (detected)
                {
                    confidence = detectionConfidence;
                    Debug.WriteLine("Check,value:{0}", confidence);
                    start_time = System.Environment.TickCount;
                }
                else
                {
                    Debug.WriteLine("Uncheck,value:{0}", confidence);
                    if (start_time != -1)
                    {
                        end_time = System.Environment.TickCount;
                        run_time = end_time - start_time;
                        if (run_time < 1000)
                        {
                            KinectClickDownEvent();
                            KinectClickUpEvent();
                        }
                        else
                        {
                            KinectClickDownRightEvent();
                            KinectClickUpRightEvent();
                        }
                    }
                }

            }
        }
    }
}
