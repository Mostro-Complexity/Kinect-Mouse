using Microsoft.Kinect;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.IO;
using System.Reflection;
using Commons.Filter;

namespace MousePanel.Controls
{
    public class MouseControl : Canvas2D.Controls.BasicControl
    {
        private float variance, sensitivity;
        private int hitTimes;
        private Queue<double> track;
        Joint thumbRight, handRight, handTipRight, wristRight;
        StreamWriter streamWriter;
        public delegate void KinectClickEventHandler();
        public event KinectClickEventHandler KinectClickUpEvent, KinectClickDownEvent,
            KinectClickUpRightEvent, KinectClickDownRightEvent;

        KalmanFilter kalmanFilter;

        /// <summary>
        /// 坐标信息
        /// </summary>
        public string DataInfo { get => dataInfo; }

        /// <summary>
        /// 点击次数
        /// </summary>
        public int HitTimes { get => hitTimes; }

        /// <summary>
        /// 右手5个轨迹点
        /// </summary>
        public Queue<double> Track { get => track; }

        /// <summary>
        /// 触键灵敏度
        /// </summary>
        public float Sensitivity { get => sensitivity; set => sensitivity = value; }
        public Joint ThumbRight { get => thumbRight; }
        public Joint HandRight { get => handRight; }
        public Joint WristRight { get => wristRight; }
        public Joint HandTipRight { get => handTipRight; }

        public float Variance { get => variance; set => variance = value; }

        public MouseControl() : base()
        {
            track = new Queue<double>();
            string fileName = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + ".txt";
            sensitivity = 0.0002f;
            thumbRight = new Joint();
            streamWriter = new StreamWriter(fileName);
            kalmanFilter = new KalmanFilter(1e-6f, 4e-4f);
        }
        public override void Start()
        {
            device.Start();
        }

        public override void Close()
        {
            device.Close();
        }

        ~MouseControl()
        {
            device.Close();
            streamWriter.Close();
        }

        public override void Draw()
        {
            if (atLarge == null) return;
            thumbRight = atLarge[0].Joints[JointType.ThumbRight];
            handRight = atLarge[0].Joints[JointType.HandRight];
            wristRight = kalmanFilter.Fetch(atLarge[0].Joints[JointType.WristRight]);//经过滤波处理
            handTipRight = atLarge[0].Joints[JointType.HandTipRight];

            dataInfo = string.Format("右手位置：({0},{1})", handRight.Position.X,
                handRight.Position.Y);
            streamWriter.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}",
                handRight.Position.X, handRight.Position.Y,
                handTipRight.Position.X, handTipRight.Position.Y, handTipRight.Position.Z,
                thumbRight.Position.X, thumbRight.Position.Y, thumbRight.Position.Z,
                wristRight.Position.X, wristRight.Position.Y);
            //Debug.WriteLine(GetTipDistance(handTipRight, thumbRight));
            Debug.WriteLine("{0} {1}", wristRight.Position.X, wristRight.Position.Y);

        }



    }
}