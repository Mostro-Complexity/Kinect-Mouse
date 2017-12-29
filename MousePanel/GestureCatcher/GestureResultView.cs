using Microsoft.Kinect;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MousePanel.GestureCatcher {
    public class GestureResultView {
        public delegate void ClickHandler();

        public event ClickHandler LeftClickUpEvent, LeftClickDownEvent,
            RightClickUpEvent, RightClickDownEvent;

        /// <summary> The body index (0-5) associated with the current gesture detector </summary>
        private int bodyIndex = 0;

        /// <summary> Current confidence value reported by the discrete gesture </summary>
        private float confidence = 0.0f;

        /// <summary> True, if the discrete gesture is currently being detected </summary>
        private bool detected = false;

        /// <summary> True, if the body is currently being tracked </summary>
        private bool isTracked = false;

        CameraSpacePoint currentPoint;

        //结构体布局 本机位置
        [StructLayout(LayoutKind.Sequential)]
        struct NativeRECT {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        //将枚举作为位域处理
        [Flags]
        enum MouseEventFlag : uint //设置鼠标动作的键值
        {
            Move = 0x0001,               //发生移动
            LeftDown = 0x0002,           //鼠标按下左键
            LeftUp = 0x0004,             //鼠标松开左键
            RightDown = 0x0008,          //鼠标按下右键
            RightUp = 0x0010,            //鼠标松开右键
            MiddleDown = 0x0020,         //鼠标按下中键
            MiddleUp = 0x0040,           //鼠标松开中键
            XDown = 0x0080,
            XUp = 0x0100,
            Wheel = 0x0800,              //鼠标轮被移动
            VirtualDesk = 0x4000,        //虚拟桌面
            Absolute = 0x8000
        }

        //设置鼠标位置
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        //设置鼠标按键和动作
        [DllImport("user32.dll")]
        static extern void mouse_event(MouseEventFlag flags, int dx, int dy,
            uint data, UIntPtr extraInfo); //UIntPtr指针多句柄类型

        /// <summary>
        /// Initializes a new instance of the GestureResultView class and sets initial property values
        /// </summary>
        /// <param name="bodyIndex">Body Index associated with the current gesture detector</param>
        /// <param name="isTracked">True, if the body is currently tracked</param>
        /// <param name="detected">True, if the gesture is currently detected for the associated body</param>
        /// <param name="confidence">Confidence value for detection of the 'Seated' gesture</param>
        public GestureResultView(int bodyIndex, bool isTracked, bool detected, float confidence) {
            this.bodyIndex = bodyIndex;
            this.isTracked = isTracked;
            this.detected = detected;
            this.confidence = confidence;
            currentPoint.X = 0;
            currentPoint.Y = 0;
        }

        /// <summary>
        /// 更新与离散手势检测结果相关的值
        /// </summary>
        /// <param name="isBodyTrackingIdValid">
        /// 如果与GestureResultView对象相关的主体仍在被追踪为True
        /// </param>
        /// <param name="isGestureDetected">
        /// 如果当前检测到相关联的身体的离散手势为True
        /// </param>
        /// <param name="detectionConfidence">
        /// 用于检测离散手势的置信度值
        /// </param>
        public virtual void UpdateGestureResult(bool isBodyTrackingIdValid,
            bool isGestureDetected, float detectionConfidence) {
            isTracked = isBodyTrackingIdValid;
            confidence = 0.0f;
            if (!isTracked) {
                detected = false;
                Debug.WriteLine("Miss");
                LeftUp();
            } else {
                detected = isGestureDetected;
                if (detected) {
                    confidence = detectionConfidence;
                    Debug.WriteLine("Check,value:{0}", confidence);
                    LeftDown();
                } else {
                    Debug.WriteLine("Uncheck,value:{0}", confidence);
                    LeftUp();
                }
            }
        }

        private void LeftDown() {
            mouse_event(MouseEventFlag.LeftDown, 0, 0, 0, UIntPtr.Zero);
            LeftClickDownEvent?.Invoke();
        }

        private void LeftUp() {
            mouse_event(MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);
            LeftClickUpEvent?.Invoke();
        }

        private void RightUp() {
            mouse_event(MouseEventFlag.RightUp, 0, 0, 0, UIntPtr.Zero);
            RightClickUpEvent?.Invoke();
        }

        private void RightDown() {
            mouse_event(MouseEventFlag.RightDown, 0, 0, 0, UIntPtr.Zero);
            RightClickDownEvent?.Invoke();
        }

        /// <summary>
        /// 更新鼠标位置
        /// </summary>
        /// <param name="nextPoint"> 下一个鼠标的位置点 </param>
        public void UpdatePosition(CameraSpacePoint nextPoint) {
            nextPoint.X *= 300;
            nextPoint.Y *= 300;
            mouse_event(MouseEventFlag.Move, 10 * Convert.ToInt32(nextPoint.X - currentPoint.X),
              -10 * Convert.ToInt32(nextPoint.Y - currentPoint.Y), 0, UIntPtr.Zero);
            currentPoint.X = nextPoint.X;
            currentPoint.Y = nextPoint.Y;
        }
    }
}
