using Microsoft.Kinect;
using System.Collections.Generic;

namespace Canvas2D.Controls {
    /// <summary>
    /// Control基类，未经允许不可改动
    /// </summary>
    public abstract class BasicControl {
        protected Canvas3D.Controls.clsKinect device = Canvas3D.Controls.clsKinect.Device;
        protected Commons.GlobalMaterials material;
        protected string dataInfo;
        protected Body[] atLarge;
        public event Panel2D.DrawGameHandler DrawGameEvent;

        /// <summary>
        /// 打开设备
        /// </summary>
        virtual public void Start() { device.Close(); }

        /// <summary>
        /// 关闭设备
        /// </summary>
        virtual public void Close() { device.Start(); }

        virtual public void Frame_Arrived(object sender, BodyFrameArrivedEventArgs e) {
            bool dataReceived = false;
            using (BodyFrame bodyframe = e.FrameReference.AcquireFrame()) {
                if (bodyframe != null) {
                    if (atLarge == null) {
                        atLarge = new Body[bodyframe.BodyCount];
                    }
                    bodyframe.GetAndRefreshBodyData(atLarge);
                    dataReceived = true;
                }
            }
            if (!dataReceived)
                return;
            foreach (var _body in atLarge) {
                if (_body.IsTracked) {
                    IReadOnlyDictionary<JointType, Joint> joints = _body.Joints;
                    DrawGameEvent();
                }
            }
        }

        /// <summary>
        /// 绘制函数
        /// </summary>
        abstract public void Draw();

        public BasicControl() {
            device.FrameArrivedHandler += Frame_Arrived;
            material = new Commons.GlobalMaterials();
        }
    }
}
