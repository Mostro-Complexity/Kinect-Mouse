using Microsoft.Kinect;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.IO;
using System.Reflection;

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
        public event KinectClickEventHandler KinectClickUpEvent, KinectClickDownEvent;

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
            wristRight = atLarge[0].Joints[JointType.WristRight];
            handTipRight = atLarge[0].Joints[JointType.HandTipRight];

            dataInfo = string.Format("右手位置：({0},{1})", handRight.Position.X,
                handRight.Position.Y);
            streamWriter.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}",
                handRight.Position.X, handRight.Position.Y,
                handTipRight.Position.X, handTipRight.Position.Y, handTipRight.Position.Z,
                thumbRight.Position.X, thumbRight.Position.Y, thumbRight.Position.Z,
                wristRight.Position.X, wristRight.Position.Y);
            //Debug.WriteLine(GetTipDistance(handTipRight, thumbRight));
            GetTrack();
        }

        public double GetTipDistance(Joint a, Joint b)
        {
            return Math.Sqrt(
            Convert.ToDouble(
             (a.Position.X - b.Position.X) * (a.Position.X - b.Position.X) +
                 (a.Position.Y - b.Position.Y) * (a.Position.Y - b.Position.Y) +
                 (a.Position.Z - b.Position.Y) * (a.Position.Z - b.Position.Z)));
        }

        /// <summary>
        /// 记录轨迹数组
        /// </summary>
        private void GetTrack(int maxsize = 15)
        {
            if (track.Count < maxsize)
            {
                track.Enqueue(GetTipDistance(handTipRight, thumbRight));
                return;
            }
            else
            {
                track.Dequeue();
                track.Enqueue(GetTipDistance(handTipRight, thumbRight));
            }
            int down = 0, up = 0;
            foreach (var i in track)
            {
                if (i != double.NaN && i > 0.05)
                    up++;
                if (i == double.NaN || i < 0.05)
                    down++;

                Debug.WriteLine("{0} {1}", down, up);
            }
            //Debug.WriteLine(Convert.ToDouble(counter));
            // 点击条件
            if (Convert.ToDouble(up) > 0.75 * Convert.ToDouble(maxsize))
            {
                up = 0;
                track.Clear();
                KinectClickUpEvent?.Invoke();
            }
            else
            {
                if (Convert.ToDouble(down) > 0.75 * Convert.ToDouble(maxsize))
                {
                    Debug.WriteLine("点击次数" + hitTimes);
                    track.Clear();
                    hitTimes += 1;
                    KinectClickDownEvent?.Invoke();
                    down = 0;
                }
            }
        }

        private float GetVariance()
        {
            float ave = 0, variance = 0;
            foreach (float i in track)
            {
                ave += i;
            }
            ave /= track.Count;
            foreach (float i in track)
            {
                variance += (i - ave) * (i - ave) / track.Count;
            }
            return variance;
        }
    }
}