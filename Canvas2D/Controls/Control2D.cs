using Microsoft.Kinect;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace Canvas2D.Controls {
    public class Control2D : BasicControl {
        private int hitTimes, counter, sensitivity;
        private Queue<float> track;
        private string content;
        private float variance;
        Joint rightHand;


        public Joint RightHand { get => rightHand; }

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
        public Queue<float> Track { get => track; }

        /// <summary>
        /// 触键灵敏度
        /// </summary>
        public int Sensitivity { get => sensitivity; set => sensitivity = value; }

        public float Variance { get => variance; set => variance = value; }

        public Control2D() : base() {//TODO: 修改
            track = new Queue<float>();
            sensitivity = 20;
        }

        public Control2D(string XMLpath) : base() {//TODO: 修改
            track = new Queue<float>();
            sensitivity = 20;
        }

        /// <summary>
        /// 打开设备
        /// </summary>
        override public void Start() {
            device.Start();
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        override public void Close() {
            device.Close();
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~Control2D() {
            device.Close();
        }

        private void SendSingleKey() {
            if (content == null) return;
            string[] keyText = new string[2];
            if (content.Length > 1 && content.Contains(" "))
                keyText = content.Split(' ');
            switch (content) {
                // TODO:功能键区
                case "isChinese": break;
                case "clear": break;
                case "delete": break;
                case "send": break;
                case "space": break;
                default: break;
            }
        }

        /// <summary>
        /// 记录轨迹数组
        /// </summary>
        private void GetTrack() {
            int startTime = System.Environment.TickCount;
            if (track.Count <= 5)
                track.Enqueue(rightHand.Position.Z);
            else {
                track.Enqueue(rightHand.Position.Z);
                track.Dequeue();
            }
            variance = GetVariance();

            // Debug.WriteLine(tmp);
            // 点击条件
            if (variance > sensitivity) {
                counter += 1;
                // 来回挥手算是一次点击
                if (counter >= 2) {
                    hitTimes += 1;
                    counter = 0;
                    int endTime = System.Environment.TickCount;
                    int runTime = startTime - endTime;
                    if (runTime >= 3000)
                        hitTimes = 1;
                    SendSingleKey();
                }
                Debug.WriteLine("点击次数" + hitTimes);
                for (int i = 0; i < track.Count; i++) {
                    track.Dequeue();
                }
            }
        }

        /// <summary>
        /// 计算轨迹数组的方差
        /// </summary>
        /// <returns></returns>
        private float GetVariance() {
            float ave = 0, variance = 0;
            foreach (float i in track) {
                ave += i;
            }
            ave /= track.Count;
            foreach (float i in track) {
                variance += (i - ave) * (i - ave) / track.Count;
            }
            return variance;
        }

        /// <summary>
        /// 绘制函数
        /// </summary>
        override public void Draw() {
            if (atLarge != null) {
                rightHand = atLarge[0].Joints[JointType.HandRight];
                dataInfo = string.Format("右手位置：({0},{1})",
                    rightHand.Position.X, rightHand.Position.Y);
                GetTrack();
                Models.Point point = new Models.Point(
                    rightHand.Position.X, rightHand.Position.Y);
                point.Draw2D();
            }
        }
    }
}
