using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using OpenTK;
using static OpenTK.Graphics.Glu;
using OpenTK.Graphics.OpenGL;
using static System.Math;

namespace Canvas3D.Controls
{
    /// <summary>
    /// 单个身体
    /// </summary>
    public class clsBody
    {
        private Dictionary<JointType, clsVector3> jointsMap;//骨骼转化为空间坐标点的字典
        private clsBones tuples = clsBones.getTuples();
        private double scale;

        /// <summary>
        /// 放大倍数
        /// </summary>
        public double Scale { get => scale; set => scale = value; }

        public Dictionary<JointType, clsVector3> JointsMap { get => jointsMap; }

        public clsBody()
        {
            jointsMap = new Dictionary<JointType, clsVector3>();
            scale = 100;
        }

        /// <summary>
        /// 连接骨骼节点
        /// </summary>
        public void Draw()
        {
            foreach (var i in jointsMap)
            {
                i.Value.CoordinateTransform();
                i.Value.Draw();
            }

            for (int i = 0; i < clsBones.count - 1; i++)
            {
                Tuple<JointType, JointType> tempTuple = tuples.getTuple(i);
                clsVector3 x = jointsMap[tempTuple.Item1],//起点
                y = jointsMap[tempTuple.Item2];//终点
                DrawBone(x, y);
            }
        }

        /// <summary>
        /// 某一时刻所有骨骼节点
        /// </summary>
        /// <returns></returns>
        public List<Tuple<clsVector3, clsVector3>> Shot()
        {
            List<Tuple<clsVector3, clsVector3>> nodeList = new List<Tuple<clsVector3, clsVector3>>();
            for (int i = 0; i < clsBones.count - 1; i++)
            {
                Tuple<JointType, JointType> tempTuple = tuples.getTuple(i);
                nodeList.Add(new Tuple<clsVector3, clsVector3>(jointsMap[tempTuple.Item1]
                    , jointsMap[tempTuple.Item2]));
            }
            return nodeList;
        }

        private Vector3 mul(double a, Vector3 s)
        {
            s.X *= (float)a;
            s.Y *= (float)a;
            s.Z *= (float)a;
            return s;
        }

        private void DrawBone(clsVector3 x, clsVector3 y)
        {
            Vector3 a = x.getVec();
            Vector3 b = y.getVec();
            IntPtr pObj = NewQuadric();
            GL.PushMatrix();
            Vector3 o = new Vector3(0, 0, 1);
            Vector3 xy = new Vector3(b - a);
            double cosa = (o.X * xy.X + o.Y * xy.Y + o.Z * xy.Z)
                / (Sqrt(xy.X * xy.X + xy.Y * xy.Y + xy.Z * xy.Z));
            double acra = Atan(-cosa / Sqrt(-cosa * cosa + 1)) + 2 * Atan(1);
            acra = acra * 180 / PI;
            double length = Sqrt((b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) *
                (b.Y - a.Y) + (b.Z - a.Z) * (b.Z - a.Z));
            GL.PushMatrix();
            GL.Translate(mul(Scale, a));
            Vector3 norm = Vector3.Cross(o, (b - a));
            norm.Normalize();

            GL.Rotate(acra, norm.X, norm.Y, norm.Z);
            Cylinder(pObj, 0.5, 0.5, length * scale, 10, 1);
            GL.PopMatrix();
            pObj = IntPtr.Zero;
            GL.PopMatrix();
            pObj = IntPtr.Zero;
        }

        public void Append(Joint j)
        {
            jointsMap.Add(j.JointType, new clsVector3(j.Position));
        }
    }
}
